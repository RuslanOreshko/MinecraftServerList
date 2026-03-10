namespace ServerList.Application.Abstractions.Services;


public interface ICheckerFactory
{
    IMinecraftServerCheker Create();
}