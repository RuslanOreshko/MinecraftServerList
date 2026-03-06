using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public sealed class SortByOnlineStrategy : IServerQueryStrategy
{
    public IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        if (!string.Equals(filter.SortBy, "online", StringComparison.CurrentCultureIgnoreCase)) return query;

        return query.OrderByDescending(x => x.OnlinePlayers);
    }
}