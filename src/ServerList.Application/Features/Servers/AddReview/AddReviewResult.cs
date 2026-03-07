namespace ServerList.Application.Features.Server.AddReview;


public sealed record AddReviewResult(
    Guid ReviewId,
    Guid ServerId,
    Guid UserId,
    string Text,
    DateTime CreatedAt
);