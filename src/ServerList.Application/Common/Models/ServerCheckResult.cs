namespace ServerList.Application.Common.Models;

public sealed record ServerChekerResult(
    bool IsOnline,
    int OnlinePlayers
);