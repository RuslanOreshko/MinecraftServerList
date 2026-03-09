using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IUseRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken ct);

    Task AddAsync(User user, CancellationToken ct);
}