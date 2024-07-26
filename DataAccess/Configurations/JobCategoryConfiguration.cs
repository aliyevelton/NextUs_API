using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
{
    public void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        builder.Property(c => c.Name).IsRequired(true).HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(500);

        builder.HasMany(c => c.Jobs)
            .WithOne(j => j.Category)
            .HasForeignKey(j => j.CategoryId);
    }
}
