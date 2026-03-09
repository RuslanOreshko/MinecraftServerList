using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Features.Auth.Login;
using ServerList.Application.Features.Auth.RefreshTokens;
using ServerList.Application.Features.Auth.Register;

namespace ServerList.Api.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUseCase _registerUseCase;
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;

    public AuthController(
        IRegisterUseCase registerUseCase,
        ILoginUseCase loginUserCase,
        IRefreshTokenUseCase refreshTokenUseCase
    )
    {
        _registerUseCase = registerUseCase;
        _loginUseCase = loginUserCase;
        _refreshTokenUseCase = refreshTokenUseCase;
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

    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login(
        [FromBody] LoginRequest request,
        CancellationToken ct
    )
    {
        var result = await _loginUseCase.ExecuteAsync(request, ct);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenResult>> Refresh(
        [FromBody] RefreshTokenRequest request,
        CancellationToken ct
    )
    {
        var result = await _refreshTokenUseCase.ExecuteAsync(request, ct);

        return Ok(result);
    }
}