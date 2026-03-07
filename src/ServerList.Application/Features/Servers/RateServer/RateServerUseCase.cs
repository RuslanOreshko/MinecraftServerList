using System.Runtime.CompilerServices;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.RateServer;


public sealed class RateServerUseCase : IRateServerUseCase
{ 
    private readonly IGameServerRepository _server;
    private readonly IRatingRepository _rating;
    private readonly IUnitOfWork _uow;

    public RateServerUseCase(
        IGameServerRepository server,
        IRatingRepository rating,
        IUnitOfWork uow
    )
    {
        _server = server;
        _rating = rating;
        _uow = uow;
    }

    public async Task<RateServerResult> ExecuteAsync(
        Guid serverId,
        int stars,
        Guid userId,
        CancellationToken ct
    )
    {
        if (stars < 1 || stars > 5)
            throw new ArgumentException("Stars must be between 1 nad 5.");

        var server = await _server.GetByIdAsync(serverId, ct);
        if (server is null)
            throw new KeyNotFoundException("Server not found.");

        var existingRating = await _rating.GetUserRatingAsync(serverId, userId, ct);

        if (existingRating is null)
        {
            await _rating.AddAsync(new Rating
            {
                ServerId = serverId,
                UserId = userId,
                Stars = stars
            }, ct);
        }
        else
        {
            existingRating.Stars = stars;
            existingRating.UpdateAt = DateTime.UtcNow;
        }

        await _uow.SaveChangesAsync(ct);

        server.AverageRating = await _rating.GetAverageAsync(server.Id, ct);
        server.RatingCount = await _rating.GetCountAsync(server.Id, ct);

        _server.Update(server);

        await _uow.SaveChangesAsync(ct);

        return new RateServerResult(
            server.Id,
            server.AverageRating,
            server.RatingCount
        );
    }   
}