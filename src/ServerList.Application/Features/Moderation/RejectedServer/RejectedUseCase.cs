using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Common.Exceptions;
using ServerList.Domain.Enums;

namespace ServerList.Application.Features.Moderation.RejectedServer;


public sealed class RejectedUseCase : IRejectedUsecCase
{
    private readonly IGameServerRepository _gameServerRepository;
    private readonly IUnitOfWork _uow;

    public RejectedUseCase(
        IGameServerRepository gameServerRepository,
        IUnitOfWork uow
    )
    {
        _gameServerRepository = gameServerRepository;
        _uow = uow;
    }

    public async Task ExecuteAsync(Guid serverId, CancellationToken ct)
    {
        var server = await _gameServerRepository.GetByIdAsync(serverId, ct);

        if(server is null)
            throw new NotFoundException("Server not found.");

        if(server.ModerationStatus != ModerationStatus.Pending)
            throw new ConflictException("Server is not pending moderation.");

        server.ModerationStatus = ModerationStatus.Rejected;

        _gameServerRepository.Update(server);

        await _uow.SaveChangesAsync(ct);
    }
}