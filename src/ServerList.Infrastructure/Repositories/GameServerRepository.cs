using Microsoft.EntityFrameworkCore;
using ServerList.Domain.Entities;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Infrastructure.Persistence;

namespace ServerList.Infrastructure.Repositories;


public sealed class GameServerRepository : IGameServerRepository
{
    private readonly AppDbContext _db;
    public GameServerRepository(AppDbContext db) => _db = db;

    public Task<bool> ExistsByIpPortAsync(string ip, int port, CancellationToken ct) 
        => _db.GameServers.AnyAsync(x => x.Ip == ip && x.Port == port, ct);

    public Task AddAsync(GameServer server, CancellationToken ct)
        => _db.GameServers.AddAsync(server, ct).AsTask();

    public IQueryable<GameServer> Query() => _db.GameServers.AsNoTracking();

    public Task<GameServer?> GetByIdAsync(Guid id, CancellationToken  ct) 
        => _db.GameServers.FirstOrDefaultAsync(x => x.Id == id, ct);

    public void Update(GameServer server) 
        => _db.GameServers.Update(server);
}