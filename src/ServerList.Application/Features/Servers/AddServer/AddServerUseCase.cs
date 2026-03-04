using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;

namespace ServerList.Application.Features.Server.AddServer;


public sealed class AddServerUseCase : IAddServerUseCase
{
    private readonly IGameServerRepository _servers;
    private readonly IUnitOfWork _uow;


    public AddServerUseCase(
        IGameServerRepository server,
        IUnitOfWork uow
    )
    {
        _servers = server;
        _uow = uow;
    }

    public async Task<AddServerResult> ExecuteAsync(
        AddServerRequest request,
        Guid userId,
        CancellationToken ct
    )
    {
        if (await _servers.ExistsByIpPortAsync(request.Ip, request.Port, ct))
            throw new Exception("Server alredy exists");

        var server  = new GameServer
        {
            Name = request.Name,
            Ip = request.Ip,
            Port = request.Port,
            Country = request.Country,
            Mode = request.Mode,
            Version = request.Version,
            Descriptions = request.Description ?? "",
            CreatedByUserId = userId
        };

        await _servers.AddAsync(server, ct);

        await _uow.SaveChangesAsync(ct);

        return new AddServerResult(server.Id);
    }
}