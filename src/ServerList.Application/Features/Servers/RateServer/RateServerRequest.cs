namespace ServerList.Application.Features.Server.RateServer;


public sealed record RateServerRequest(
    Guid ServerId,
    int Stars
);