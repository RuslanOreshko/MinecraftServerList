using ServerList.Application.Common.Pagination;
using ServerList.Application.Features.Server.GetServerReviews;


public interface IGetServerReviewUseCase
{
    Task<PagedResult<ServerReviewItemDto>> ExecuteAsync(
        Guid serverId,
        GetServerReviewFilter filter,
        CancellationToken ct
    );
}