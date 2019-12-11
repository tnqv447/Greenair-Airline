using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Configuration
{
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasKey(s => s.AirportId);

            builder.Property(s => s.AirportId).HasMaxLength(3);
            builder.Property(s => s.AirportName).HasMaxLength(30).IsRequired();

            builder.HasMany<Route>(s => s.RouteStarts)
                .WithOne(a => a.OrgAirport)
                .HasForeignKey(a => a.Origin)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Route>(s => s.RouteEnds)
                .WithOne(a => a.DesAirport)
                .HasForeignKey(a => a.Destination)
                .OnDelete(DeleteBehavior.Cascade);

            //------------------------------------------------------------------
            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(x => x.Num).HasColumnName("AddressNum");
                a.Property(x => x.Street).HasColumnName("Street");
                a.Property(x => x.District).HasColumnName("District");
                a.Property(x => x.City).HasColumnName("City");
                a.Property(x => x.State).HasColumnName("State");
                a.Property(x => x.Country).HasColumnName("Country");
            });
        }
    }
}