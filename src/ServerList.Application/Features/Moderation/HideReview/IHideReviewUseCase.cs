namespace ServerList.Application.Features.Moderation.HideReview;


public interface IHideReviewUseCase
{
    Task<HideReviewResult> ExecuteAsync(
        Guid reviewId,
        string reason,
        CancellationToken ct
    );
}