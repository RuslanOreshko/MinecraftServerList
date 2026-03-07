using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _db;

    public ReviewRepository(AppDbContext db) => _db = db;

    public Task AddAsync(Review review, CancellationToken ct)
        => _db.Reviews.AddAsync(review, ct).AsTask();
}