using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Common.Pagination;
using ServerList.Application.Features.Server.AddServer;
using ServerList.Application.Features.Server.SearchServers;
using ServerList.Application.Features.Server.RateServer;
using ServerList.Application.Features.Server.SearchServers.Abstractions;
using ServerList.Application.Features.Server.AddReview;
using ServerList.Application.Features.Server.GetServerReviews;
using Microsoft.AspNetCore.Authorization;
using ServerList.Api.Common.Extensions;
using ServerList.Infrastructure.Services.Minecraft;


namespace ServerList.Api.Controllers;


[ApiController]
[Route("api/v1/server")]
public sealed class ServerController : ControllerBase
{
    private readonly IAddServerUseCase _useCase;
    private readonly ISearchServerUseCase _searchServers;
    private readonly IRateServerUseCase _rateServer;
    private readonly IAddReviewUseCase _addReview;
    private readonly IGetServerReviewUseCase _getReviews;

    public ServerController
    (
        IAddServerUseCase useCase,
        ISearchServerUseCase searchServers,
        IRateServerUseCase rateServer,
        IAddReviewUseCase addReview,
        IGetServerReviewUseCase getReviews
    )
    {
        _useCase = useCase;
        _searchServers = searchServers;
        _rateServer =  rateServer;
        _addReview = addReview;
        _getReviews = getReviews;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AddServerResult>> Add(AddServerRequest request, CancellationToken ct)
    {
        var userId = User.GetUserId();
        var result = await _useCase.ExecuteAsync(request, userId, ct);
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

    // Rating server
    [Authorize]
    [HttpPost("{id:guid}/rating")]
    public async Task<ActionResult<RateServerResult>> Rate(
        Guid id,
        [FromBody] RateServerRequest request,
        CancellationToken ct)
    {
        var userId = User.GetUserId();

        var result = await _rateServer.ExecuteAsync(
            id,
            request.Stars,
            userId,
            ct
        );

        return Ok(result);
    }

    // Add Review to server
    [Authorize]
    [HttpPost("{id:guid}/reviews")]
    public async Task<ActionResult<AddReviewResult>> AddReview(
        Guid id,
        [FromBody] AddReviewRequest request,
        CancellationToken ct
    )
    {
        var userId = User.GetUserId();

        var result = await _addReview.ExecuteAsync(
            id,
            request.Text,
            userId,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}/reviews")]
    public async Task<ActionResult<PagedResult<ServerReviewItemDto>>> GetReviews(
        Guid id,
        [FromQuery] GetServerReviewFilter filter,
        CancellationToken ct
    )
    {
        var result = await _getReviews.ExecuteAsync(id, filter, ct);

        return Ok(result);
    }




    [HttpGet("test-chek")]
    public async Task<IActionResult> TestCheck(
        [FromQuery] string ip,
        [FromQuery] int port,
        CancellationToken ct
    )
    {
        var cheker = new JavaServerChecker();

        var result = await cheker.CheckAsync(ip, port, ct);

        return Ok(result);
    }
}