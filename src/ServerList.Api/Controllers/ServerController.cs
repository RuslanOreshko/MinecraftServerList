using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Common.Pagination;
using ServerList.Application.Features.Server.AddServer;
using ServerList.Application.Features.Server.SearchServers;

namespace ServerList.Controllers;


[ApiController]
[Route("api/v1/server")]
public sealed class ServerController : ControllerBase
{
    private readonly IAddServerUseCase _useCase;
    private readonly ISearchServerUseCase _searchServers;

    public ServerController
    (
        IAddServerUseCase useCase,
        ISearchServerUseCase searchServers
    )
    {
        _useCase = useCase;
        _searchServers = searchServers;
    }

    [HttpPost]
    public async Task<ActionResult<AddServerResult>> Add(AddServerRequest request, CancellationToken ct)
    {
        var faceUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var result = await _useCase.ExecuteAsync(request, faceUserId, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ServerListItemDto>>> Search(
        [FromQuery] ServerSearchFilter filter,
        CancellationToken ct
    )
    {
        var result = await _searchServers.ExecuteAsync(filter, ct);

        return Ok(result);
    }
}