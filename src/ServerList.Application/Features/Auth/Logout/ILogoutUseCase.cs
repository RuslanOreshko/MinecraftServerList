namespace ServerList.Application.Features.Auth.Logout;


public interface ILogoutUseCase
{
    Task ExecuteAsync(LogoutRequest request, CancellationToken ct);
}