using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Enums;

namespace PageBuilder.Infrastructure.Data.Configurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("Pages");

        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.PageStatus).IsRequired().HasDefaultValue(PageStatus.Draft);
        builder.Property(p => p.DeletedAt).IsRequired(false);

        // Relationships
        builder
            .HasOne(p => p.Company)
            .WithMany(c => c.Pages)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Blocks).WithOne(b => b.Page).HasForeignKey(b => b.PageId);

        // Soft delete query filter
        builder.HasQueryFilter(p => p.DeletedAt == null);

        // Indexes
        builder.HasIndex(p => new { p.CompanyId });

        builder.HasIndex(p => p.CreatedAt);
        builder
            .HasIndex(p => new { p.CompanyId, p.PageStatus })
            .IsUnique()
            .HasFilter("[PageStatus] = 1 AND [DeletedAt] IS NULL");
    }
}
