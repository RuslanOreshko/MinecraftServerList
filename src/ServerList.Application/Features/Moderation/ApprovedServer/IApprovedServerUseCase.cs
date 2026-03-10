namespace ServerList.Application.Features.Moderation.ApprovedServer;


public interface IApprovedServerUseCase
{
    Task ExecuteAsync(Guid serverId, Guid moderatoeId, CancellationToken ct);
}