using Microsoft.AspNetCore.Http;

namespace ServerList.Api.Authentication;

public class RefreshTokenCookieService : IRefreshTokenCookieService
{
    private const string CookieName = "refreshToken";
    private const string CookiePath = "api/v1/auth";

    public string? GetRefreshToken(HttpRequest request)
    {
        request.Cookies.TryGetValue(CookieName, out var refreshToken);
        return refreshToken;
    }

    public void SetRefreshToken(HttpResponse response, string refreshToken, DateTime expiresAtUtc)
    {
        response.Cookies.Append(CookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = expiresAtUtc,
            Path = CookiePath
        });
    }

    public void DeleteRefreshToken(HttpResponse response)
    {
        response.Cookies.Delete(CookieName, new CookieOptions
        {
            Path = CookiePath
        });
    }
}