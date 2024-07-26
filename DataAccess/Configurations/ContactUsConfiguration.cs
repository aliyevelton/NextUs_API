using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
{
    public void Configure(EntityTypeBuilder<ContactUs> builder)
    {
        builder.Property(c => c.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.Surname).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.Email).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.Message).IsRequired(true).HasMaxLength(1000);
    }
}
