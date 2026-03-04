using Microsoft.Extensions.DependencyInjection;
using ServerList.Application.Features.Server.AddServer;

namespace ServerList.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddServerUseCase, AddServerUseCase>();

        return services;
    }
}