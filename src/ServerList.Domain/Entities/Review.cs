namespace ServerList.Domain.Entities;


public sealed class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ServerId { get; set; }
    public Guid UserId { get; set; }

    public string Text { get; set; } = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; }
    
    public bool IsHidden { get; set; }
    public string? HiddenReason { get; set; }
    public Guid? HiddenByModeratorId { get; set; }

    public GameServer Server { get; set; } = default!;
}

