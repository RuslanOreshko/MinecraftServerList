using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken ct);
    Task<User?> GetByEmailWithRolesAsync(string email, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
}