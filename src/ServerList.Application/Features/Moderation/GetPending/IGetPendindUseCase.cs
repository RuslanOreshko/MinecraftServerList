using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Moderation.GetPending;


public interface IGetPendingUseCase
{
    Task<List<PendingServerResult>> ExecuteAsync(CancellationToken ct);
}