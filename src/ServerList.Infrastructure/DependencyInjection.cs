using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;
using ServerList.Infrastructure.Repositories;

namespace ServerList.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        var cs = cfg.GetConnectionString("Postgres") 
            ?? throw new InvalidOperationException("Missing connections string: Postgres");

        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cs));

        services.AddScoped<IGameServerRepository, GameServerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEf>();

        // Rating repo
        services.AddScoped<IRatingRepository, RatingRepository>();

        // Review repo
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}