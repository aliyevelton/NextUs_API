using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(c => c.Title).IsRequired(true).HasMaxLength(75);
        builder.Property(c => c.Description).IsRequired(true).HasMaxLength(5000);
        builder.Property(c => c.Price).HasColumnType("decimal(18, 2)");
        builder.Property(c => c.Picture).HasMaxLength(200);
        builder.Property(c => c.CategoryId).IsRequired(true);
        builder.Property(c => c.CompanyId).IsRequired(true);
        builder.Property(c => c.CourseType).IsRequired(true);
        builder.Property(c => c.Location).IsRequired(true).HasMaxLength(30);
        builder.Property(c => c.TotalHours).IsRequired(true);

        builder.HasMany(c => c.Tags)
               .WithOne(ct => ct.Course)
               .HasForeignKey(ct => ct.CourseId);
    }
}
