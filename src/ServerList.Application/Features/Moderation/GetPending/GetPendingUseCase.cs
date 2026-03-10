using Microsoft.EntityFrameworkCore;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Enums;

namespace ServerList.Application.Features.Moderation.GetPending;


public sealed class GetPendingUseCase : IGetPendingUseCase
{
    private readonly IGameServerRepository _gameServerRepo;

    public GetPendingUseCase(
        IGameServerRepository gameServerRepository
    )
    {
        _gameServerRepo = gameServerRepository;
    }

    public async Task<List<PendingServerResult>> ExecuteAsync(CancellationToken ct)
    {
        return await _gameServerRepo.Query()
            .Where(x => x.ModerationStatus == ModerationStatus.Pending)
            .Select(x => new PendingServerResult(
                x.Id,
                x.Name,
                x.Ip,
                x.Port,
                x.Mode,
                x.Version,
                x.CreatedAt
            ))
            .ToListAsync(ct);
    }
}