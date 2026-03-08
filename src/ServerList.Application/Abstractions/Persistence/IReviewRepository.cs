using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IReviewRepository
{
    Task AddAsync(Review review, CancellationToken ct);
    Task<Review?> GetByIdAsync(Guid id, CancellationToken ct);
    void Update(Review review);
}