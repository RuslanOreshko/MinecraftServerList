using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence.Configuration;


public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> b)
    {
        b.ToTable("roles");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        b.HasIndex(x => x.Name).IsUnique();

        b.HasData(
            new Role { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "User" },
            new Role { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Moderator" },
            new Role { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Admin" }
        );
    }
}