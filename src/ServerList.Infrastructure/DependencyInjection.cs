using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Infrastructure.Persistence;
using ServerList.Infrastructure.Repositories;

namespace ServerList.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        var cs = cfg.GetConnectionString("Postgres") 
            ?? throw new InvalidOperationException("Missing connections string: Postgres");

        services.AddDbContext<AppDbContex>(opt => opt.UseNpgsql(cs));

        services.AddScoped<IGameServerRepository, GameServerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEf>();

        return services;
    }
}