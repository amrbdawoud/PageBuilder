using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageBuilder.Domain.Entities;

namespace PageBuilder.Infrastructure.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Table name
        builder.ToTable("Companies");

        // Primary key
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.DeletedAt).IsRequired(false);

        // Relationships
        builder.HasMany(c => c.Pages).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);

        // Soft delete query filter
        builder.HasQueryFilter(c => c.DeletedAt == null);

        // Indexes
        builder.HasIndex(c => c.Name).IsUnique();
    }
}
