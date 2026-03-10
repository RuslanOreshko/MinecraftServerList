using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Features.Moderation.HideReview;
using ServerList.Application.Features.Moderation.GetPending;

namespace ServerList.Api.Controllers;


[ApiController]
[Route("api/v1/moderator")]
public sealed class ModerationController : ControllerBase
{
    private readonly IHideReviewUseCase _hideReview;
    private readonly IGetPendindUseCase _getPendingUseCase;

    public ModerationController(
        IHideReviewUseCase hideReview,
        IGetPendindUseCase getPendindUseCase
    )
    {
        _hideReview = hideReview;
        _getPendingUseCase = getPendindUseCase;
    }

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

    [HttpGet("server/pending")]
    public async Task<ActionResult<PendingServerResult>> GetPending(
        CancellationToken ct
    )
    {
        var result = await _getPendingUseCase.ExecuteAsync(ct);

        return Ok(result);
    }
}