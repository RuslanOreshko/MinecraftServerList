namespace ServerList.Application.Features.Server.RateServer;


public interface IRateServerUseCase
{
    Task<RateServerResult> ExecuteAsync(
        Guid serverId,
        int stars,
        Guid userId,
        CancellationToken ct
    );
}