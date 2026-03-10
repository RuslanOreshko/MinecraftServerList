using ServerList.Application.Abstractions.Persistance;

namespace ServerList.Application.Features.Moderation.HideReview;


public sealed class HideReviewUseCase : IHideReviewUseCase
{
    private readonly IReviewRepository _reviews;
    private readonly IUnitOfWork _uow;

    public HideReviewUseCase(
        IReviewRepository reviews,
        IUnitOfWork uow
    )
    {
        _reviews = reviews;
        _uow = uow;
    }

    public async Task<HideReviewResult> ExecuteAsync(
        Guid reviewId,
        string reason,
        CancellationToken ct
    )
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Hide reason is requires.");

        var review = await _reviews.GetByIdAsync(reviewId, ct);
        if(review is null)
            throw new KeyNotFoundException("Review not found.");

        review.IsHidden = true;
        review.HiddenReason = reason;
        review.UpdateAt = DateTime.UtcNow;

        _reviews.Update(review);

        await _uow.SaveChangesAsync(ct);

        return new HideReviewResult(
            review.Id,
            review.IsHidden,
            review.HiddenReason,
            review.HiddenByModeratorId
        );
    }
}