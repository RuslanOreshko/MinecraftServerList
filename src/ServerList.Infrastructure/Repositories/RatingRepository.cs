using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class RatingRepository : IRatingRepository
{
    private readonly AppDbContext _db;

    public RatingRepository(AppDbContext db) => _db = db;

    public Task<Rating?> GetUserRatingAsync(Guid serverId, Guid userId, CancellationToken ct)
        => _db.Ratings.FirstOrDefaultAsync(x => x.ServerId == serverId && x.UserId == userId);

    public Task AddAsync(Rating rating, CancellationToken ct) 
        => _db.Ratings.AddAsync(rating, ct).AsTask();

    public async Task<decimal> GetAverageAsync(Guid serverId, CancellationToken ct)
    {
        var avg = await _db.Ratings
            .Where(x => x.ServerId == serverId)
            .AverageAsync(x => (double?)x.Stars, ct);

        return avg is null ? 0 : (decimal)avg.Value;
    }

    public Task<int> GetCountAsync(Guid serverId, CancellationToken ct)
        => _db.Ratings.CountAsync(x => x.ServerId == serverId, ct);
}