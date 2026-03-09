using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name, CancellationToken ct);
}