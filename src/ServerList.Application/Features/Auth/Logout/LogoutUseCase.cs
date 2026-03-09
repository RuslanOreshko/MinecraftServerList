using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Abstractions.Security;


namespace ServerList.Application.Features.Auth.Logout;


public sealed class LogoutUseCase : ILogoutUseCase
{
    private readonly IRefreshTokenHasher _refreshTokenHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly IUnitOfWork _uow;


    public LogoutUseCase(
        IRefreshTokenHasher refreshTokenHasher,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork uow
    )
    {
        _refreshTokenHasher = refreshTokenHasher;
        _refreshTokenRepo = refreshTokenRepository;
        _uow = uow;
    }

    public async Task ExecuteAsync(LogoutRequest request, CancellationToken ct)
    {
         var tokenHash = _refreshTokenHasher.Hash(request.RefreshToken);

        var activeToken = await _refreshTokenRepo.GetActiveByTokenHashAsync(tokenHash, ct);

        if (activeToken is null)
            return;

        var revoked = DateTime.UtcNow;

        await _refreshTokenRepo.RevokeAsync(activeToken.Id, revoked, ct);
        await _uow.SaveChangesAsync(ct);
    }
}