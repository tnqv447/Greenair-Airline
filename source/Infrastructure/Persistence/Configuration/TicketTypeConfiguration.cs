using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            builder.HasKey(c => c.TicketTypeId);

            builder.Property(s => s.TicketTypeId).HasMaxLength(3);
            builder.Property(s => s.TicketTypeName).HasMaxLength(20).IsRequired();
            builder.Property(s => s.BasePrice).HasColumnType("decimal(18,2)");

        }
    }
}