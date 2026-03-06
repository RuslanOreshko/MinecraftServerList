using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence.Configuration;


public sealed class GameServerConfiguration : IEntityTypeConfiguration<GameServer>
{
    public void Configure(EntityTypeBuilder<GameServer> b)
    {
        b.ToTable("game_server");
        b.HasKey(x => x.Id);

        b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        b.Property(x => x.Ip).IsRequired().HasMaxLength(64);

        b.HasIndex(x => new { x.Ip, x.Port }).IsUnique();
    }
}



