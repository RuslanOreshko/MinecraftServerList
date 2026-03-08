using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Common.Pagination;

namespace ServerList.Application.Features.Server.GetServerReviews;


public sealed class GetServerReviewUseCase : IGetServerReviewUseCase
{
    private readonly IGameServerRepository _servers;
    private readonly IReviewRepository _reviews;

    public GetServerReviewUseCase(
        IGameServerRepository servers,
        IReviewRepository reviews
    )
    {
        _servers = servers;
        _reviews = reviews;
    }

    public async Task<PagedResult<ServerReviewItemDto>> ExecuteAsync(
        Guid serverId,
        GetServerReviewFilter filter,
        CancellationToken ct
    )
    {
        var server = await _servers.GetByIdAsync(serverId, ct);
        if(server is null)
            throw new KeyNotFoundException("Server not found.");

        var page = filter.Page < 1 ? 1 : filter.Page;
        var pageSize = filter.PageSize < 1 ? 20 : filter.PageSize;

        var query = _reviews.Query()
            .Where(x => x.ServerId == serverId && !x.IsHidden)
            .OrderByDescending(x => x.CreatedAt);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ServerReviewItemDto(
                x.Id,
                x.UserId,
                x.Text,
                x.CreatedAt
            ))
            .ToListAsync();

        return new PagedResult<ServerReviewItemDto>(
            items,
            total,
            page,
            pageSize
        );
    }
}