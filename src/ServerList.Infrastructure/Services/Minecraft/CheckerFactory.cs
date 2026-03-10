using ServerList.Application.Abstractions.Services;

namespace ServerList.Infrastructure.Services.Minecraft;


public sealed class CheckerFactory : ICheckerFactory
{
    public IMinecraftServerCheker Create()
    {
        return new JavaServerChecker();
    }
}