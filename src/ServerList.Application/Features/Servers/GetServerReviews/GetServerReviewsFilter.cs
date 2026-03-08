namespace ServerList.Application.Features.Server.GetServerReviews;


public sealed class GetServerReviewFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}