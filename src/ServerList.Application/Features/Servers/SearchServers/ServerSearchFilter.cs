namespace ServerList.Application.Features.Server.SearchServers;

public sealed record ServerSearchFilter(
    string? Country,
    string? Mode,
    string? Version,
    decimal? MinRating,
    string SortBy = "newest",
    int Page = 1,
    int PageSize = 20
);