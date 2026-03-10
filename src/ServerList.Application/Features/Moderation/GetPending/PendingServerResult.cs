namespace ServerList.Application.Features.Moderation.GetPending;


public sealed record PendingServerResult(
    Guid Id,
    string Name, 
    string Ip,
    int Port,
    string Mode,
    string Version,
    DateTime CreatedAt
);