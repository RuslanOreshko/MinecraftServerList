namespace ServerList.Application.Features.Auth.Login;


public sealed record LoginResult(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);