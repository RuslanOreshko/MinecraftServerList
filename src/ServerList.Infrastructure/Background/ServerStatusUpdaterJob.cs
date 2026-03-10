using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Abstractions.Services;
using ServerList.Domain.Enums;

namespace ServerList.Infrastructure.Background;


public sealed class ServerStatusUpdaterJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ServerStatusUpdaterJob> _logger;

    public ServerStatusUpdaterJob(
        IServiceScopeFactory scopeFactory,
        ILogger<ServerStatusUpdaterJob> logger
    )
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await UpdaterServerAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating server statuses.");
            }

            await Task.Delay(TimeSpan.FromSeconds(15), ct);
        }
    }

    private async Task UpdaterServerAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();

        var serverRepository = scope.ServiceProvider.GetRequiredService<IGameServerRepository>();
        var checkerFactory = scope.ServiceProvider.GetRequiredService<ICheckerFactory>();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var servers = await serverRepository.GetApprovedAsync(ct);

        foreach(var server in servers)
        {
            try
            {
                var checker = checkerFactory.Create();
                var result = await checker.CheckAsync(server.Ip, server.Port, ct);

                server.ServerStatus = result.IsOnline
                    ? ServerStatus.Online
                    : ServerStatus.offline;

                server.OnlinePlayers = result.IsOnline
                    ? result.OnlinePlayers
                    : 0;

                server.LastCheckAt = DateTime.UtcNow;

                serverRepository.Update(server);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to check server {ServerId} ({Ip}:{Port})", server.Id, server.Ip, server.Port);

                server.ServerStatus = ServerStatus.offline;
                server.OnlinePlayers = 0;
                server.LastCheckAt = DateTime.UtcNow;

                serverRepository.Update(server);
            }
        }

        await uow.SaveChangesAsync(ct);
    }
}