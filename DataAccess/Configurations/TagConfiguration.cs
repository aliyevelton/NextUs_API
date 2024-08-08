using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name).IsRequired(true).HasMaxLength(50);

        builder.HasMany(t => t.CourseTags)
               .WithOne(ct => ct.Tag)
               .HasForeignKey(ct => ct.TagId);

        builder.HasMany(t => t.JobTags)
               .WithOne(jt => jt.Tag)
               .HasForeignKey(jt => jt.TagId);
    }
}