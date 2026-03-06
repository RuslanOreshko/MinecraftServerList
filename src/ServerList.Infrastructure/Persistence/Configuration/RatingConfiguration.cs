using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerList.Domain.Entities;

namespace ServerList.Infrastructure.Persistence.Configuration;


public sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> b)
    {
        b.ToTable("ratings");
        b.HasKey(x => new { x.ServerId, x.UserId });

        b.Property(x => x.Stars).IsRequired();

        b.HasOne(x => x.Server)
            .WithMany()
            .HasForeignKey(x => x.ServerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}



