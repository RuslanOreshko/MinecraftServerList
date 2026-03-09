namespace ServerList.Application.Features.Auth.Register;


public sealed record RegisterResult(
    Guid UserId,
    string Email,
    string UserName,
    DateTime CreatedAt
);