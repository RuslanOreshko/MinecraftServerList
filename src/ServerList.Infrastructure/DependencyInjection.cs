using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerList.Application.Abstractions.Persistance;
using ServerList.Application.Abstractions.Security;
using ServerList.Domain.Entities;
using ServerList.Infrastructure.Persistence;
using ServerList.Infrastructure.Repositories;
using ServerList.Infrastructure.Security;

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

        // User and Role repo 
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // password hasher 
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        // Jwt
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        services.Configure<JwtOptions>(
            cfg.GetSection(JwtOptions.SectionName));

        return services;
    }
}