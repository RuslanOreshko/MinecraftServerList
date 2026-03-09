using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Features.Auth.Register;

namespace ServerList.Api.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUseCase _registerUseCase;

    public AuthController(
        IRegisterUseCase registerUseCase
    )
    {
        _registerUseCase = registerUseCase;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResult>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken ct
    )
    {
        var result = await _registerUseCase.ExecuteAsync(request, ct);

        return Ok(result);
    }
}