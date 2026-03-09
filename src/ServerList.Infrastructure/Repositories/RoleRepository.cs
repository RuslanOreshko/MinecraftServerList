using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository(
        AppDbContext db
    )
    {
        _db = db;
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name, ct);
    }
}