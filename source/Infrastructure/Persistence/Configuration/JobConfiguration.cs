using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationCore.Entities;
namespace Infrastructure.Persistence.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(c => c.JobId);

            builder.Property(s => s.JobId).HasMaxLength(3);
            builder.Property(s => s.JobName).HasMaxLength(20).IsRequired();

        }
    }
}