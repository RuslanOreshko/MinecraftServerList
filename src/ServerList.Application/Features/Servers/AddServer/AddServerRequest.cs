namespace ServerList.Application.Features.Server.AddServer;


public sealed record AddServerRequest(
    string Name,
    string Ip,
    int Port,
    string Country,
    string Mode,
    string Version,
    string? Description
);