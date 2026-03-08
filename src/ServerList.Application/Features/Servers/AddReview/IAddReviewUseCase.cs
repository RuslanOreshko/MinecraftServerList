namespace ServerList.Application.Features.Server.AddReview;


public interface IAddReviewUseCase
{
    Task<AddReviewResult> ExecuteAsync(
        Guid serverId,
        string text,
        Guid userId,
        CancellationToken ct
    );
}