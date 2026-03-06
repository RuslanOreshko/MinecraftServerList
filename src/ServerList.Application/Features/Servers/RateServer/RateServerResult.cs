namespace ServerList.Application.Features.Server.RateServer;


public sealed record RateServerResult(
    Guid ServerId,
    decimal AverageRating,
    int RatingCount
);