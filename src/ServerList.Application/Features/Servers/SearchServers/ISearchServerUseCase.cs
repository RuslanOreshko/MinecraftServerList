using ServerList.Application.Common.Pagination;

namespace ServerList.Application.Features.Server.SearchServers;


public interface ISearchServerUseCase
{
    Task<PagedResult<ServerListItemDto>> ExecuteAsync(ServerSearchFilter filter, CancellationToken ct);
}
