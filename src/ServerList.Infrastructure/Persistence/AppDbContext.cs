using Microsoft.EntityFrameworkCore;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence;


public sealed class AppDbContex : DbContext
{
    public AppDbContex(DbContextOptions<AppDbContex> options) : base(options){}

    public DbSet<GameServer> GameServers => Set<GameServer>();
    public DbSet<Rating> Ratings => Set<Rating>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameServer>(b =>
        {
            b.ToTable("game_server");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.Property(x => x.Ip).IsRequired().HasMaxLength(64);

            b.HasIndex(x => new { x.Ip, x.Port }).IsUnique();
        });

        modelBuilder.Entity<Rating>(b =>
        {
            b.ToTable("ratings");
            b.HasKey(x => new { x.ServerId, x.UserId });

            b.Property(x => x.Stars).IsRequired();

            b.HasOne(x => x.Server)
                .WithMany()
                .HasForeignKey(x => x.ServerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}