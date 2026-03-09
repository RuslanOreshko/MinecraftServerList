using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _db;

    public RefreshTokenRepository(
        AppDbContext db
    )
    {
        _db = db;
    }

    public async Task AddAsync(RefreshToken token, CancellationToken ct)
    {
        await _db.RefreshTokens.AddAsync(token, ct);
    }

    public async Task<RefreshToken?> GetActiveByTokenHashAsync(string tokenHash, CancellationToken ct)
    {
        var now = DateTime.UtcNow;

        return await _db.RefreshTokens
            .Include(x => x.User)
                .ThenInclude(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => 
                x.TokenHash == tokenHash &&
                x.RevokedAt == null &&
                x.ExpiresAt > now,
                ct);
    }

    public async Task RevokeAsync(Guid tokenId, DateTime revokedAtUtc, CancellationToken ct)
    {
        var token = await _db.RefreshTokens
            .FirstOrDefaultAsync(x => x.Id == tokenId, ct);

        if (token is null)
            return;

        token.RevokedAt = revokedAtUtc;
    }

    public async Task RevokeAllByUserIdAsync(Guid userId, DateTime revokedAtUtc, CancellationToken ct)
    {
        var tokens = await _db.RefreshTokens
            .Where(x =>
                x.UserId == userId &&
                x.RevokedAt == null &&
                x.ExpiresAt > revokedAtUtc)
            .ToListAsync();

        foreach(var token in tokens)
        {
            token.RevokedAt = revokedAtUtc;
        }
    }
}