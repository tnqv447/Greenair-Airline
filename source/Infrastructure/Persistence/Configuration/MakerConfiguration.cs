using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Configuration
{
    public class MakerConfiguration : IEntityTypeConfiguration<Maker>
    {
        public void Configure(EntityTypeBuilder<Maker> builder)
        {
            builder.HasKey(s => s.MakerId);
            builder.Property(s => s.MakerId).HasMaxLength(3);

            builder.Property(s => s.MakerName).HasMaxLength(20);

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