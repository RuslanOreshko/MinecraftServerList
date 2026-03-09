namespace ServerList.Application.Features.Auth.Login;


public sealed record LoginRequest(
    string Email,
    string Password,
    string? DeviceInfo
);