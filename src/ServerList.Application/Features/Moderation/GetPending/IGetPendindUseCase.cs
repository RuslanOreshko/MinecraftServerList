using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Moderation.GetPending;


public interface IGetPendindUseCase
{
    Task<List<PendingServerResult>> ExecuteAsync(CancellationToken ct);
}