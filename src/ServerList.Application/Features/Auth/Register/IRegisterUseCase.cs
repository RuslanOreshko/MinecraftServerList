namespace ServerList.Application.Features.Auth.Register;


public interface IRegisterUseCase
{
    Task<RegisterResult> ExecuteAsync(
        RegisterRequest request,
        CancellationToken ct
    );
}