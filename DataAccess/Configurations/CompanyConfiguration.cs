using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(c => c.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.About).IsRequired(false).HasMaxLength(3000);
        builder.Property(c => c.Website).IsRequired(false).HasMaxLength(50);
        builder.Property(c => c.Email).IsRequired(true).HasMaxLength(50);
        builder.Property(c => c.Phone).IsRequired(true).HasMaxLength(15);
    }
}
