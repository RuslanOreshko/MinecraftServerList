namespace ServerList.Infrastructure.Security;


public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public int AccessTokenLifetimeMinutes { get; set; }
}