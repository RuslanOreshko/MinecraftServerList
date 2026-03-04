using Microsoft.EntityFrameworkCore;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence;


public sealed class AppDbContex : DbContext
{
    public AppDbContex(DbContextOptions<AppDbContex> options) : base(options){}

    public DbSet<GameServer> GameServers => Set<GameServer>();

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
    }
}