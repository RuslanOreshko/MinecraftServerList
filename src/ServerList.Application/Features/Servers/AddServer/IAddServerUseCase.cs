namespace ServerList.Application.Features.Server.AddServer;


public interface IAddServerUseCase
{
    Task<AddServerResult> ExecuteAsync(
        AddServerRequest request,
        Guid userId,
        CancellationToken ct
    );
}