namespace ServerList.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserName { get; set; } = default!;
    public string Email { get; set;} = default!;
    public string PasswordHash { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsBlocked { get; set; }
}