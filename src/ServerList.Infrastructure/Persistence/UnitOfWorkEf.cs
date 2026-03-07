using ServerList.Application.Abstractions.Persistance;

namespace ServerList.Infrastructure.Persistence;


public sealed class UnitOfWorkEf : IUnitOfWork
{
    private readonly AppDbContext _db;

    public UnitOfWorkEf(AppDbContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}