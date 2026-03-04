namespace ServerList.Application.Abstractions.Persistance;


public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct);
}