using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence.Configuration;


public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> b)
    {
        b.ToTable("refresh_tokens");

        b.HasKey(x => x.Id);

        b.Property(x => x.TokenHash)
            .IsRequired()
            .HasMaxLength(500);

        b.Property(x => x.CreatedAt)
            .IsRequired();

        b.Property(x => x.ExpiresAt)
            .IsRequired();

        b.Property(x => x.DeviceInfo)
            .HasMaxLength(500);

        b.HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.UserId);
        b.HasIndex(x => x.ExpiresAt);
    }
}