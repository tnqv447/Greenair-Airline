using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(c => new { c.TicketId, c.FlightId });

            builder.Property(s => s.TicketId).HasMaxLength(5);
            builder.Property(s => s.AssignedCus).HasMaxLength(30);

            builder.Property(s => s.TicketTypeId).IsRequired();
            builder.HasOne<TicketType>(s => s.TicketType)
                .WithMany(a => a.Tickets)
                .HasForeignKey(s => s.TicketTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(s => s.CustomerId).IsRequired();
            builder.HasOne<Customer>(s => s.Customer)
                .WithMany(a => a.Tickets)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}