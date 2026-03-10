namespace ServerList.Application.Features.Moderation.RejectedServer;


public interface IRejectedUsecCase
{
    Task ExecuteAsync(Guid serverId, CancellationToken ct);
}