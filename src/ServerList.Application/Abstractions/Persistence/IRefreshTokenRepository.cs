using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token, CancellationToken ct);
    Task<RefreshToken?> GetActiveByTokenHashAsync(string tokenHash, CancellationToken ct);
    Task RevokeAsync(Guid tokenId, DateTime revokedAtUtc, CancellationToken ct);
    Task RevokeAllByUserIdAsync(Guid userId, DateTime revokedAtUtc, CancellationToken ct);
}