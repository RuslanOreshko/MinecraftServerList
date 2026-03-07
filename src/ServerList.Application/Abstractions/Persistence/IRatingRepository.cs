using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IRatingRepository
{
    Task<Rating?> GetUserRatingAsync(Guid serverId, Guid userId, CancellationToken ct);
    Task AddAsync(Rating rating, CancellationToken ct);
    Task<decimal> GetAverageAsync(Guid serverId, CancellationToken ct);
    Task<int> GetCountAsync(Guid serverId, CancellationToken ct);
}