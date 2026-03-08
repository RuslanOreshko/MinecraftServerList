namespace ServerList.Application.Features.Moderation.HideReview;


public sealed record HideReviewResult(
    Guid ReviewId,
    bool IsHidden,
    string? HiddenReason,
    Guid? HiddenByModerator
);