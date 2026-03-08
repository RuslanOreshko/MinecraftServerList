using System.Runtime.CompilerServices;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.AddReview;


public sealed class AddReviewUseCase : IAddReviewUseCase
{
    private readonly IGameServerRepository _server;
    private readonly IReviewRepository _reviews;
    private readonly IUnitOfWork _uow;

    public AddReviewUseCase(
        IGameServerRepository server,
        IReviewRepository reviews,
        IUnitOfWork uow
    )
    {
        _server = server;
        _reviews = reviews;
        _uow = uow;
    }

    public async Task<AddReviewResult> ExecuteAsync(
        Guid serverId,
        string text,
        Guid userId,
        CancellationToken ct
    )
    {
        if(string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Review text is required.");

        var server = await _server.GetByIdAsync(serverId, ct);
        if (server is null)
            throw new KeyNotFoundException("Server not found.");

        var review = new Review
        {
            ServerId = serverId,
            UserId = userId,
            Text = text.Trim()
        };

        await _reviews.AddAsync(review, ct);
        await _uow.SaveChangesAsync(ct);

        return new AddReviewResult(
            review.Id,
            review.ServerId,
            review.UserId,
            review.Text,
            review.CreatedAt
        );
    }
}