using ServerList.Application.Features.Server.SearchServers;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public sealed class FilterByCountryStrategy : IServerQueryStrategy
{
    public IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        return string.IsNullOrWhiteSpace(filter.Country)
            ? query
            : query.Where(x => x.Country == filter.Country);
    }
}