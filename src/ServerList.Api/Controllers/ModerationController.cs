using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Features.Moderation.HideReview;
using ServerList.Application.Features.Moderation.GetPending;
using ServerList.Application.Features.Moderation.ApprovedServer;
using Microsoft.AspNetCore.Authorization;
using ServerList.Application.Features.Moderation.RejectedServer;

namespace ServerList.Api.Controllers;


[ApiController]
[Route("api/v1/moderator")]
public sealed class ModerationController : ControllerBase
{
    private readonly IHideReviewUseCase _hideReview;
    private readonly IGetPendindUseCase _getPendingUseCase;
    private readonly IApprovedServerUseCase _approveServerUseCase;
    private readonly IRejectedUsecCase _rejectedUsecCase;

    public ModerationController(
        IHideReviewUseCase hideReview,
        IGetPendindUseCase getPendindUseCase,
        IApprovedServerUseCase approvedServerUseCase,
        IRejectedUsecCase rejectedUsecCase
    )
    {
        _hideReview = hideReview;
        _getPendingUseCase = getPendindUseCase;
        _approveServerUseCase = approvedServerUseCase;
        _rejectedUsecCase = rejectedUsecCase;
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPatch("reviews/{id:guid}/hide")]
    public async Task<ActionResult<HideReviewResult>> HideReview(
        Guid id,
        [FromBody] HideReviewRequest request,
        CancellationToken ct
    )
    {
        var fakeModeratorId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var result = await _hideReview.ExecuteAsync(
            id,
            request.Reason,
            fakeModeratorId,
            ct
        );

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpGet("server/pending")]
    public async Task<ActionResult<PendingServerResult>> GetPending(
        CancellationToken ct
    )
    {
        var result = await _getPendingUseCase.ExecuteAsync(ct);

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost("server/{id:guid}/approve")]
    public async Task<ActionResult> ApproveServer(Guid id, CancellationToken ct)
    {
        await _approveServerUseCase.ExecuteAsync(id, ct);

        return NoContent();
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost("server/{id:guid}/reject")]
    public async Task<ActionResult> RejectServer(Guid id, CancellationToken ct)
    {
        await _rejectedUsecCase.ExecuteAsync(id, ct);

        return NoContent();
    }

}