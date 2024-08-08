using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations;

public class CourseTagConfiguration : IEntityTypeConfiguration<CourseTag>
{
    public void Configure(EntityTypeBuilder<CourseTag> builder)
    {
        builder.HasKey(ct => new { ct.CourseId, ct.TagId });

        builder.HasOne(ct => ct.Course)
               .WithMany(c => c.Tags)
               .HasForeignKey(ct => ct.CourseId);

        builder.HasOne(ct => ct.Tag)
               .WithMany(t => t.CourseTags)
               .HasForeignKey(ct => ct.TagId);
    }
}
