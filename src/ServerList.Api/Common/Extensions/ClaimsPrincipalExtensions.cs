using System.Security.Claims;

namespace ServerList.Api.Common.Extensions;


public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if(id is null)    
            throw new UnauthorizedAccessException("User id claims missing");

        return Guid.Parse(id);
    }
}