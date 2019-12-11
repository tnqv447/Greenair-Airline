//using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne<Account>(x => x.Account)
                .WithOne(y => y.Person)
                .HasForeignKey<Account>(y => y.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Id).HasMaxLength(5);
            builder.Property(s => s.LastName).HasMaxLength(20).IsRequired();
            builder.Property(s => s.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(s => s.Phone).HasMaxLength(10).IsRequired();



            //----------------------------------------------------------------------------------
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
