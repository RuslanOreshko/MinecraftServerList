using ServerList.Domain.Enums;

namespace ServerList.Domain.Entities;

public class GameServer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = default!;
    public string Ip { get; set; } = default!;
    public int Port { get; set; }

    public string Country { get; set; } = default!;
    public string Mode { get; set; } = default!;
    public string Version { get; set; } = default!;
    public string Descriptions { get; set; } = default!;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedByUserId { get; set; }

    public ServerStatus Status { get; set; } = ServerStatus.Pending;
    public int OnlinePlayers { get; set; } 
    public DateTime? LastCheckAt { get; set; }

    public decimal AverageRating { get; set; }
    public int RatingCount { get; set; }
}