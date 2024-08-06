using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class JobBookmarkConfiguration : IEntityTypeConfiguration<JobBookmark>
{
    public void Configure(EntityTypeBuilder<JobBookmark> builder)
    {
        builder.Property(jb => jb.JobId).IsRequired(true);
        builder.Property(jb => jb.UserId).IsRequired(true);

        builder.HasOne(jb => jb.Job)
            .WithMany(j => j.Bookmarks)
            .HasForeignKey(jb => jb.JobId);

        builder.HasOne(jb => jb.User)
            .WithMany(u => u.Bookmarks)
            .HasForeignKey(jb => jb.UserId);
    }
}
