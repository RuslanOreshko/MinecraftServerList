using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Common.Pagination;
using ServerList.Application.Features.Server.SearchServers.Abstractions;
using ServerList.Domain.Enums;


namespace ServerList.Application.Features.Server.SearchServers;

public sealed class SearchServerUseCase : ISearchServerUseCase
{
    private readonly IGameServerRepository _db;
    private readonly IServerQueryPipeline _pipeline;

    public SearchServerUseCase
    (
        IGameServerRepository db,
        IServerQueryPipeline pipeline
    )
    {
        _db = db;
        _pipeline = pipeline;
    }

    public async Task<PagedResult<ServerListItemDto>> ExecuteAsync(ServerSearchFilter filter, CancellationToken ct)
    {
        var query = _db.Query();

        query = _pipeline.ApplyAll(query, filter);

        var total = await query.CountAsync(ct);

        var page = filter.Page < 1 ? 1 : filter.Page;
        var size = filter.PageSize is < 1 or > 50 ? 20 : filter.PageSize;

        var items = await query
            .Where(x => x.ModerationStatus == ModerationStatus.Approved)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new ServerListItemDto(
                x.Id, x.Name, x.Ip, x.Port,
                x.Country, x.Mode, x.Version,
                x.ServerStatus, x.OnlinePlayers,
                x.AverageRating, x.RatingCount
            ))
            .ToListAsync();

        return new PagedResult<ServerListItemDto>(items, total, page, size);
    }
}