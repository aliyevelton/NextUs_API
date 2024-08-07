﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Property(j => j.Title).IsRequired(true).HasMaxLength(75);
        builder.Property(j => j.Description).IsRequired(true).HasMaxLength(5000);
        builder.Property(j => j.Position).IsRequired(true).HasMaxLength(75);
        builder.Property(j => j.Location).IsRequired(true).HasMaxLength(30);
        builder.Property(j => j.JobType).IsRequired(true).HasMaxLength(30);
        builder.Property(j => j.CategoryId).IsRequired(true);
        builder.Property(j => j.CompanyId).IsRequired(true);
        builder.Property(j => j.MinSalary).HasColumnType("decimal(18, 2)");
        builder.Property(j => j.MaxSalary).HasColumnType("decimal(18, 2)");
        builder.Property(j => j.ExactSalary).HasColumnType("decimal(18, 2)");

        builder.HasMany(c => c.Tags)
               .WithOne(ct => ct.Job)
               .HasForeignKey(ct => ct.JobId);
    }
}
