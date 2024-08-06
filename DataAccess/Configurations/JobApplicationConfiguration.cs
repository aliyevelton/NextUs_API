using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.Property(ja => ja.CoverLetter).IsRequired(false).HasMaxLength(5000);
        builder.Property(ja => ja.Cv).IsRequired(true).HasMaxLength(255);
        builder.Property(ja => ja.JobId).IsRequired(true);
        builder.Property(ja => ja.UserId).IsRequired(true);

        builder.HasOne(ja => ja.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(ja => ja.JobId);

        builder.HasOne(ja => ja.User)
            .WithMany(u => u.Applications)
            .HasForeignKey(ja => ja.UserId);
    }
}
