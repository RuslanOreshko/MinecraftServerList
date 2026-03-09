namespace ServerList.Application.Features.Auth.RefreshTokens;


public sealed record RefreshTokenResult(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);