using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class FlightDetailConfiguration : IEntityTypeConfiguration<FlightDetail>
    {
        public void Configure(EntityTypeBuilder<FlightDetail> builder)
        {
            builder.HasKey(c => new { c.FlightDetailId, c.FlightId });

            builder.Property(s => s.RouteId).IsRequired();


        }
    }
}