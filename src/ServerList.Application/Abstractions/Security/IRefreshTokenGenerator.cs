namespace ServerList.Application.Abstractions.Security;


public interface IRefreshTokenGenerator
{
    string Generate();
}