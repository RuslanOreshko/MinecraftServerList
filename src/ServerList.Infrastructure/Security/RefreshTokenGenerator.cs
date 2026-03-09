using System.Security.Cryptography;
using ServerList.Application.Abstractions.Security;

namespace ServerList.Infrastructure.Security;


public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}