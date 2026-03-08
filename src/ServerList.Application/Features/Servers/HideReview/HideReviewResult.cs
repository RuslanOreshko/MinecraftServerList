namespace ServerList.Application.Features.Server.HideReview;


public sealed record HideReviewResult(
    Guid ReviewId,
    bool IsHidden,
    string? HiddenReason,
    Guid? HiddenByModerator
);