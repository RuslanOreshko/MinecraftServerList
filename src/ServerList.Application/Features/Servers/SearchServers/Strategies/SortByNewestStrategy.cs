using ServerList.Application.Features.Server.SearchServers;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public sealed class SortByNewestStrategy : IServerQueryStrategy
{
    public IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        // if sortBy is rating or online, then skip further
        if (string.Equals(filter.SortBy, "rating", StringComparison.OrdinalIgnoreCase)) return query;
        if (string.Equals(filter.SortBy, "online", StringComparison.OrdinalIgnoreCase)) return query;

        return query.OrderByDescending(x => x.CreatedAt);
    }
}