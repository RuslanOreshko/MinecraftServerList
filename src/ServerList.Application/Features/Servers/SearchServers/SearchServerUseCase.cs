using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Common.Pagination;

namespace ServerList.Application.Features.Server.SearchServers;

public sealed class SearchServerUseCase : ISearchServerUseCase
{
    private readonly IGameServerRepository _db;

    public SearchServerUseCase(IGameServerRepository db) => _db=db;

    public async Task<PagedResult<ServerListItemDto>> ExecuteAsync(ServerSearchFilter filter, CancellationToken ct)
    {
        var query = _db.Query();

        if(!string.IsNullOrWhiteSpace(filter.Country))
            query = query.Where(x => x.Country == filter.Country);

        if(!string.IsNullOrWhiteSpace(filter.Mode))
            query = query.Where(x => x.Mode == filter.Mode);

        if(!string.IsNullOrWhiteSpace(filter.Version))
            query = query.Where(x => x.Version == filter.Version);

        if(filter.MinRating is not null)
            query = query.Where(x => x.AverageRating >= filter.MinRating.Value);

        query = filter.SortBy switch
        {
            "rating" => query.OrderByDescending(x => x.AverageRating).ThenByDescending(x => x.RatingCount),
            "online" => query.OrderByDescending(x => x.OnlinePlayers),
            _ => query.OrderByDescending(x => x.CreatedAt),
        };

        var total = await query.CountAsync(ct);

        var page = filter.Page < 1 ? 1 : filter.Page;
        var size = filter.PageSize is < 1 or > 50 ? 20 : filter.PageSize;

        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new ServerListItemDto(
                x.Id, x.Name, x.Ip, x.Port,
                x.Country, x.Mode, x.Version,
                x.Status, x.OnlinePlayers,
                x.AverageRating, x.RatingCount
            ))
            .ToListAsync();

        return new PagedResult<ServerListItemDto>(items, total, page, size);
    }
}