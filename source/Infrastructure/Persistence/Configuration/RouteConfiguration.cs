using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Configuration
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(s => s.RouteId);
            builder.Property(s => s.RouteId).HasMaxLength(5);

            builder.OwnsOne(x => x.FlightTime).Property(x => x.Hour).HasColumnName("FlightHour");
            builder.OwnsOne(x => x.FlightTime).Property(x => x.Minute).HasColumnName("FlightMinute");

            builder.HasIndex(x => new { x.Origin, x.Destination }).IsUnique();
            builder.Property(s => s.Origin).IsRequired();
            builder.Property(s => s.Destination).IsRequired();

            builder.HasMany<FlightDetail>(s => s.FlightDetails)
                .WithOne(a => a.Route)
                .HasForeignKey(a => a.RouteId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}