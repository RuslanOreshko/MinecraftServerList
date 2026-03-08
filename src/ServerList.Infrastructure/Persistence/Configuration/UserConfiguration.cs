using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence.Configuration;


public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("users");

        b.HasKey(x => x.Id);

        b.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);

        b.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        b.Property(x => x.PasswordHash)
            .IsRequired();

        b.HasIndex(x => x.Email).IsUnique();
        b.HasIndex(x => x.UserName).IsUnique();
    }
}