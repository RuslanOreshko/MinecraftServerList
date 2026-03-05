using ServerList.Application.Features.Server.SearchServers;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.SearchServers.Strategies;


public sealed class SortByRatingStrategy : IServerQueryStrategy
{
    public IQueryable<GameServer> Apply(IQueryable<GameServer> query, ServerSearchFilter filter)
    {
        if (!string.Equals(filter.SortBy, "rating", StringComparison.CurrentCultureIgnoreCase)) return query;

        return query.OrderByDescending(x => x.AverageRating)
            .ThenByDescending(x => x.RatingCount);
    }
}