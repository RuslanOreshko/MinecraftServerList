using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Common.Pagination;
using ServerList.Application.Features.Server.AddServer;
using ServerList.Application.Features.Server.SearchServers;
using ServerList.Application.Features.Server.RateServer;
using ServerList.Application.Features.Server.SearchServers.Abstractions;


namespace ServerList.Controllers;


[ApiController]
[Route("api/v1/server")]
public sealed class ServerController : ControllerBase
{
    private readonly IAddServerUseCase _useCase;
    private readonly ISearchServerUseCase _searchServers;
    private readonly IRateServerUseCase _rateServer;

    public ServerController
    (
        IAddServerUseCase useCase,
        ISearchServerUseCase searchServers,
        IRateServerUseCase rateServer
    )
    {
        _useCase = useCase;
        _searchServers = searchServers;
        _rateServer =  rateServer;
    }

    [HttpPost]
    public async Task<ActionResult<AddServerResult>> Add(AddServerRequest request, CancellationToken ct)
    {
        var faceUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var result = await _useCase.ExecuteAsync(request, faceUserId, ct);
        return Ok(result);
    }

    // Search filter server
    [HttpGet]
    public async Task<ActionResult<PagedResult<ServerListItemDto>>> Search(
        [FromQuery] ServerSearchFilter filter,
        CancellationToken ct
    )
    {
        var result = await _searchServers.ExecuteAsync(filter, ct);

        return Ok(result);
    }

    [HttpPost("{id:guid}/rating")]
    public async Task<ActionResult<RateServerResult>> Rate(
        Guid id,
        [FromBody] RateServerRequest request,
        CancellationToken ct)
    {
        var fakeUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var result = await _rateServer.ExecuteAsync(
            id,
            request.Stars,
            fakeUserId,
            ct
        );

        return Ok(result);
    }
}