using Microsoft.EntityFrameworkCore;
using ServerList.Domain.Entities;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class GameServerRepository : IGameServerRepository
{
    private readonly AppDbContex _db;
    public GameServerRepository(AppDbContex db) => _db = db;

    public Task<bool> ExistsByIpPortAsync(string ip, int port, CancellationToken ct) 
        => _db.GameServers.AnyAsync(x => x.Ip == ip && x.Port == port, ct);

    public Task AddAsync(GameServer server, CancellationToken ct)
        => _db.GameServers.AddAsync(server, ct).AsTask();
}