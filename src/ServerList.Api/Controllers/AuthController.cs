using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerList.Application.Features.Auth.Login;
using ServerList.Application.Features.Auth.Logout;
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
    private readonly ILogoutUseCase _logoutUseCase;

    public AuthController(
        IRegisterUseCase registerUseCase,
        ILoginUseCase loginUserCase,
        IRefreshTokenUseCase refreshTokenUseCase,
        ILogoutUseCase logoutUseCase
    )
    {
        _registerUseCase = registerUseCase;
        _loginUseCase = loginUserCase;
        _refreshTokenUseCase = refreshTokenUseCase;
        _logoutUseCase = logoutUseCase;
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

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray();

        return Ok(new
        {
            UserId = userId,
            Email = email,
            UserName = userName,
            Roles = roles
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        CancellationToken ct
    )
    {
        await _logoutUseCase.ExecuteAsync(request, ct);

        return NoContent();
    }
}