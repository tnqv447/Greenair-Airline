using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(s => s.FlightId);
            builder.Property(s => s.FlightId).HasMaxLength(5);

            builder.HasMany<FlightDetail>(s => s.FlightDetails)
                .WithOne(a => a.Flight)
                .HasForeignKey(a => a.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Ticket>(s => s.Tickets)
                .WithOne(a => a.Flight)
                .HasForeignKey(a => a.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.PlaneId).IsRequired();
            builder.HasOne<Plane>(s => s.Plane)
                .WithMany(a => a.Flights)
                .HasForeignKey(s => s.PlaneId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}