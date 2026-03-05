using ServerList.Application.Features.Server.SearchServers;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public sealed class FilterByMinRatingStrategy : IServerQueryStrategy
{
    public IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        return filter.MinRating is null
            ? query
            : query.Where(x => x.AverageRating >= filter.MinRating.Value);
    }
}