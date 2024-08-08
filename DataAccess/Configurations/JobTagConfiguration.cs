using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations;

public class JobTagConfiguration : IEntityTypeConfiguration<JobTag>
{
    public void Configure(EntityTypeBuilder<JobTag> builder)
    {
        builder.HasKey(ct => new { ct.JobId, ct.TagId });

        builder.HasOne(ct => ct.Job)
               .WithMany(c => c.Tags)
               .HasForeignKey(ct => ct.JobId);

        builder.HasOne(ct => ct.Tag)
               .WithMany(t => t.JobTags)
               .HasForeignKey(ct => ct.TagId);
    }
}
