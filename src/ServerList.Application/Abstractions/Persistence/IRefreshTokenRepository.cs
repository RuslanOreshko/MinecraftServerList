using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token, CancellationToken ct);
}