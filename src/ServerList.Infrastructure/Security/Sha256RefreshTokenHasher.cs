using System.Security.Cryptography;
using System.Text;
using ServerList.Application.Abstractions.Security;

namespace ServerList.Infrastructure.Security;


public sealed class Sha256RefreshTokenHasher : IRefreshTokenHasher
{
    public string Hash(string refreshToken)
    {
        var bytes = Encoding.UTF8.GetBytes(refreshToken);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToHexString(hashBytes);
    }
}