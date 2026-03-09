using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Abstractions.Security;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Auth.RefreshTokens;


public sealed class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IRefreshTokenRepository _refreshTokens;
    private readonly IUnitOfWork _uow;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenHasher _refreshTokenHasher;

    public RefreshTokenUseCase(
        IRefreshTokenRepository refreshToken,
        IUnitOfWork uow,
        IJwtTokenService jwtTokenService,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenHasher refreshTokenHasher
    )
    {
        _refreshTokens = refreshToken;
        _uow = uow;
        _jwtTokenService = jwtTokenService;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenHasher = refreshTokenHasher;
    }

    public async Task<RefreshTokenResult> ExecuteAsync(RefreshTokenRequest request, CancellationToken ct)
    {
        var incomingTokenHasher = _refreshTokenHasher.Hash(request.RefreshToken);

        var existigToken = await _refreshTokens.GetActiveByTokenHashAsync(incomingTokenHasher, ct);

        if(existigToken is null)   
            throw new Exception("Invalid refresh token");

        if(existigToken.User.IsBlocked)
            throw new Exception("User is blocked");

        var revokedAt = DateTime.UtcNow;

        await _refreshTokens.RevokeAsync(existigToken.Id, revokedAt, ct);

        var roles = existigToken.User.UserRoles
            .Select(x => x.Role.Name)
            .ToArray();

        var accessToken = _jwtTokenService.GenerateAccessToken(existigToken.User, roles);

        var newRefreshTokenRaw = _refreshTokenGenerator.Generate();
        var newRefreshTokenHash = _refreshTokenHasher.Hash(newRefreshTokenRaw);

        var newRefreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = existigToken.User.Id,
            TokenHash = newRefreshTokenHash,
            CreatedAt = revokedAt,
            ExpiresAt = revokedAt.AddDays(7),
            RevokedAt = null,
            DeviceInfo = existigToken.DeviceInfo
        };

        await _refreshTokens.AddAsync(newRefreshToken, ct);
        await _uow.SaveChangesAsync(ct);

        return new RefreshTokenResult(
            accessToken,
            newRefreshTokenRaw,
            _jwtTokenService.GetAccessTokenExpiryUtc()
        );
    }
}