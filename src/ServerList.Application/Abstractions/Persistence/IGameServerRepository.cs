using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Persistance;


public interface IGameServerRepository
{
    Task<bool> ExistsByIpPortAsync(string ip, int port, CancellationToken ct);
    Task AddAsync(GameServer server, CancellationToken ct);

    IQueryable<GameServer> Query();

    Task<GameServer?> GetByIdAsync(Guid id, CancellationToken ct);
    void Update(GameServer server);
} 