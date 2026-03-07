using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IReviewRepository
{
    Task AddAsync(Review review, CancellationToken ct);
}