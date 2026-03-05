using ServerList.Domain.Enums;

namespace ServerList.Application.Features.Server.SearchServers;


public sealed record ServerListItemDto(
    Guid Id,
    string Name,
    string Ip,
    int Port,
    string Country,
    string Mode,
    string Version,
    ServerStatus Status,
    int OnlinePlayers,
    decimal AverageRating,
    int RatingsCount
);