namespace ServerList.Domain.Entities;


public class Rating
{
    public Guid ServerId { get; set; }
    public Guid UserId { get; set; }

    public int Stars { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    public GameServer Server { get; set; } = default!;
}