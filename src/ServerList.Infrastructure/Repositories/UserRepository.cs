using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(
        AppDbContext db
    )
    {
        _db = db;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email, ct);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken ct)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.UserName == userName, ct);
    }

    public async Task<User?> GetByEmailWithRolesAsync(string email, CancellationToken ct)
    {
        return await _db.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _db.Users.AddAsync(user);
    }
}