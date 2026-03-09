using ServerList.Domain.Entities;

namespace ServerList.Application.Abstractions.Security;


public interface IJWTTokenService
{
    string GenerateAccessToken(User user, IReadOnlyCollection<string> roles);
    
    DateTime GetAccessTokenExpiryUtc();
}