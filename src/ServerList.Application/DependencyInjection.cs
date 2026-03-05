using Microsoft.Extensions.DependencyInjection;
using ServerList.Application.Features.Server.AddServer;
using ServerList.Application.Features.Server.SearchServers;

namespace ServerList.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddServerUseCase, AddServerUseCase>();
        services.AddScoped<ISearchServerUseCase, SearchServerUseCase>();
        
        return services;
    }
}