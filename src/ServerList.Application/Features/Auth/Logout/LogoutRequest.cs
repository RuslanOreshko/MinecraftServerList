namespace ServerList.Application.Features.Auth.Logout;


public sealed record LogoutRequest(
    string RefreshToken
);