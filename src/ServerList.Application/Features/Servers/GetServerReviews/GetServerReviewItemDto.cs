namespace ServerList.Application.Features.Server.GetServerReviews;


public sealed record ServerReviewItemDto(
    Guid ReviewId,
    Guid UserId,
    string Text,
    DateTime CreatedAt
);