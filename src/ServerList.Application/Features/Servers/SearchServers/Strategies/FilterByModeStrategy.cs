using ServerList.Application.Features.Server.SearchServers;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public sealed class FilterByModeStrategy : IServerQueryStrategy
{
    public IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        return string.IsNullOrWhiteSpace(filter.Mode)
            ? query
            : query.Where(x => x.Mode == filter.Mode);
    }
}