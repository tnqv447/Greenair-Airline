using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasBaseType<Person>();
            //builder.OwnsOne(m => m.Address);

            builder.Property(s => s.Salary).HasColumnType("decimal(18,2)");
            builder.Property(s => s.JobId).IsRequired();

            builder.HasOne<Job>(s => s.Job)
                .WithMany(a => a.Employees)
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}