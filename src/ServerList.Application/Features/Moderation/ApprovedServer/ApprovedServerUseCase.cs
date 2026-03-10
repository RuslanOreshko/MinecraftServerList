using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Common.Exceptions;
using ServerList.Domain.Enums;

namespace ServerList.Application.Features.Moderation.ApprovedServer;


public sealed class ApprovedServerUseCase : IApprovedServerUseCase
{
    private readonly IGameServerRepository _gameServerRepo;
    private readonly IUnitOfWork _uow;

    public ApprovedServerUseCase(
        IGameServerRepository gameServerRepository,
        IUnitOfWork uow
    )
    {
        _gameServerRepo = gameServerRepository;
        _uow = uow;
    }

    public async Task ExecuteAsync(Guid serverId, CancellationToken ct)
    {
        var server = await _gameServerRepo.GetByIdAsync(serverId, ct);

        if(server is null)
            throw new NotFoundException("Server not found.");

        if(server.ModerationStatus != ModerationStatus.Pending)
            throw new ConflictException("Server is not pending moderation.");

        server.ModerationStatus = ModerationStatus.Approved;

        _gameServerRepo.Update(server);
        await _uow.SaveChangesAsync(ct);
    }
}