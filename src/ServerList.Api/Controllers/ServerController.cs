using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Features.Server.AddServer;

namespace ServerList.Controllers;


[ApiController]
[Route("api/v1/server")]
public sealed class ServerController : ControllerBase
{
    public readonly IAddServerUseCase _useCase;
    public ServerController(IAddServerUseCase useCase) => _useCase = useCase;


    [HttpPost]
    public async Task<ActionResult<AddServerResult>> Add(AddServerRequest request, CancellationToken ct)
    {
        var faceUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var result = await _useCase.ExecuteAsync(request, faceUserId, ct);
        return Ok(result);
    }
}