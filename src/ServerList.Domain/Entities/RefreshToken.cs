namespace ServerList.Domain.Entities;


public sealed class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
    public string TokenHash { get; set; } = default!;
    public DateTime CreatedAt { get; set; } 
    public DateTime ExpiresAt { get; set; } 
    public DateTime? RevokedAt { get; set; } 
    public string? DeviceInfo { get; set; }
    public User User { get; set; } = default!;
}