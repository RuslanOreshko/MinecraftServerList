namespace ServerList.Application.Features.Auth.RefreshTokens;


public interface IRefreshTokenUseCase
{
    Task<RefreshTokenResult> ExecuteAsync(RefreshTokenRequest request, CancellationToken ct);
}