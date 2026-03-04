using ServerList.Application.Abstractions.Persistance;

namespace ServerList.Infrastructure.Persistence;


public sealed class UnitOfWorkEf : IUnitOfWork
{
    private readonly AppDbContex _db;

    public UnitOfWorkEf(AppDbContex db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}