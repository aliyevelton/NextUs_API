using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.Property(c => c.Name).IsRequired(true).HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(500);

        builder.HasMany(c => c.Courses)
            .WithOne(course => course.Category)
            .HasForeignKey(course => course.CategoryId);
    }
}
