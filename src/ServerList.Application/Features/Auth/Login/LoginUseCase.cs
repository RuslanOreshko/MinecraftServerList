using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Abstractions.Security;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Auth.Login;

public sealed class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _users;
    private readonly IRefreshTokenRepository _refreshTokens;
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public LoginUseCase(
        IUserRepository users,
        IRefreshTokenRepository refreshTokens,
        IUnitOfWork uow,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IRefreshTokenGenerator refreshTokenGenerator
    )
    {
        _users = users;
        _refreshTokens = refreshTokens;
        _uow = uow;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<LoginResult> ExecuteAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await _users.GetByEmailWithRolesAsync(request.Email, ct);

        if (user is null)
            throw new Exception("Invalid email or password");

        if (user.IsBlocked)
            throw new Exception("User is blocked");
        
        var passwordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            throw new Exception("Invalid email or password");

        var roles = user.UserRoles
            .Select(x => x.Role.Name)
            .ToArray();

        var accessToken = _jwtTokenService.GenerateAccessToken(user, roles);
        var refreshTokenRaw = _refreshTokenGenerator.Generate();
        var refreshTokenHash = _passwordHasher.Hash(refreshTokenRaw);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = refreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            RevokedAt = null,
            DeviceInfo = request.DeviceInfo
        };

        await _refreshTokens.AddAsync(refreshToken, ct);
        await _uow.SaveChangesAsync(ct);
        
        return new LoginResult(
            accessToken,
            refreshTokenRaw,
            _jwtTokenService.GetAccessTokenExpiryUtc()
        );
    }
}