using ServerList.Application.Common.Models;

namespace ServerList.Application.Abstractions.Services;


public interface IMinecraftServerCheker
{
    Task<ServerChekerResult> CheckAsync(string ip, int port, CancellationToken ct);
}