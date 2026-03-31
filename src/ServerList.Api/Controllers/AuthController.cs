using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerList.Api.Authentication;
using ServerList.Application.Features.Auth.Login;
using ServerList.Application.Features.Auth.Logout;
using ServerList.Application.Features.Auth.RefreshTokens;
using ServerList.Application.Features.Auth.Register;

namespace ServerList.Api.Controllers;


[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUseCase _registerUseCase;
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;
    private readonly ILogoutUseCase _logoutUseCase;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;

    public AuthController(
        IRegisterUseCase registerUseCase,
        ILoginUseCase loginUserCase,
        IRefreshTokenUseCase refreshTokenUseCase,
        ILogoutUseCase logoutUseCase,
        IRefreshTokenCookieService refreshTokenCookieService
    )
    {
        _registerUseCase = registerUseCase;
        _loginUseCase = loginUserCase;
        _refreshTokenUseCase = refreshTokenUseCase;
        _logoutUseCase = logoutUseCase;
        _refreshTokenCookieService = refreshTokenCookieService;
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

        _refreshTokenCookieService.SetRefreshToken(
            Response,
            result.RefreshToken,
            result.ExpiresAt
        );

        return Ok(new
        {
            accessToken = result.AccessToken,
            expiresAt = result.ExpiresAt
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(CancellationToken ct)
    {
        var refreshToken = _refreshTokenCookieService.GetRefreshToken(Request);

        if(string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized();

        var result = await _refreshTokenUseCase.ExecuteAsync(
            new RefreshTokenRequest(refreshToken),
            ct
        );

        _refreshTokenCookieService.SetRefreshToken(
            Response,
            result.RefreshToken,
            result.ExpiresAt
        );

        return Ok(new
        {
            accessToken = result.AccessToken,
            expiresAt = result.ExpiresAt
        });
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
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        var refreshToken = _refreshTokenCookieService.GetRefreshToken(Request);

        if(!string.IsNullOrWhiteSpace(refreshToken))
            await _logoutUseCase.ExecuteAsync(new LogoutRequest(refreshToken), ct);

        _refreshTokenCookieService.DeleteRefreshToken(Response);

        return NoContent();
    }
}