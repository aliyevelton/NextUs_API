using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CourseBookmarkConfiguration : IEntityTypeConfiguration<CourseBookmark>
{
    public void Configure(EntityTypeBuilder<CourseBookmark> builder)
    {
        builder.Property(cb => cb.CourseId).IsRequired(true);
        builder.Property(cb => cb.UserId).IsRequired(true);

        builder.HasOne(cb => cb.Course)
            .WithMany(c => c.Bookmarks)
            .HasForeignKey(cb => cb.CourseId);

        builder.HasOne(cb => cb.User)
            .WithMany(u => u.CourseBookmarks)
            .HasForeignKey(cb => cb.UserId);
    }
}
