using Microsoft.Extensions.DependencyInjection;
using ServerList.Application.Features.Auth.Login;
using ServerList.Application.Features.Auth.Register;
using ServerList.Application.Features.Moderation.HideReview;
using ServerList.Application.Features.Server.AddReview;
using ServerList.Application.Features.Server.AddServer;
using ServerList.Application.Features.Server.GetServerReviews;
using ServerList.Application.Features.Server.RateServer;
using ServerList.Application.Features.Server.SearchServers;
using ServerList.Application.Features.Server.SearchServers.Abstractions;
using ServerList.Application.Features.Server.SearchServers.Strategies;

namespace ServerList.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddServerUseCase, AddServerUseCase>();
        services.AddScoped<ISearchServerUseCase, SearchServerUseCase>();

        // add strategies to DI container
        services.AddScoped<IServerQueryStrategy, FilterByCountryStrategy>();
        services.AddScoped<IServerQueryStrategy, FilterByModeStrategy>();
        services.AddScoped<IServerQueryStrategy, FilterByVersionStrategy>();
        services.AddScoped<IServerQueryStrategy, FilterByMinRatingStrategy>();
        services.AddScoped<IServerQueryStrategy, SortByRatingStrategy>();
        services.AddScoped<IServerQueryStrategy, SortByOnlineStrategy>();
        services.AddScoped<IServerQueryStrategy, SortByNewestStrategy>();

        // pipeline for strategies
        services.AddScoped<IServerQueryPipeline, ServerQueryPipeline>();

        // rating use case DI
        services.AddScoped<IRateServerUseCase, RateServerUseCase>();

        // review use case DI
        services.AddScoped<IAddReviewUseCase, AddReviewUseCase>();
        services.AddScoped<IHideReviewUseCase, HideReviewUseCase>();
        services.AddScoped<IGetServerReviewUseCase, GetServerReviewUseCase>();

        // Auth use cases
        // Register use case
        services.AddScoped<IRegisterUseCase, RegisterUseCase>();

        // Login
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        return services;
    }
}