using System.Net;
namespace ServerList.Api.Authentication;

public interface IRefreshTokenCookieService
{
    string? GetRefreshToken(HttpRequest request);
    void SetRefreshToken(HttpResponse response, string refreshToken, DateTime expiresAtUtc);
    void DeleteRefreshToken(HttpResponse response);
}