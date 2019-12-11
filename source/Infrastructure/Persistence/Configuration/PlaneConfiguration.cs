using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class PlaneConfiguration : IEntityTypeConfiguration<Plane>
    {
        public void Configure(EntityTypeBuilder<Plane> builder)
        {
            builder.HasKey(s => s.PlaneId);
            builder.Property(s => s.PlaneId).HasMaxLength(5);

            builder.Property(s => s.MakerId).IsRequired();
            builder.HasOne<Maker>(s => s.Maker)
                .WithMany(a => a.Planes)
                .HasForeignKey(s => s.MakerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}