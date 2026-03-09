namespace ServerList.Application.Features.Auth.Login;


public interface ILoginUseCase
{
    Task<LoginResult> ExecuteAsync(LoginRequest request, CancellationToken ct);
}