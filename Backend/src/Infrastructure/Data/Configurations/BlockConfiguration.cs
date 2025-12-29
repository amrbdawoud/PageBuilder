using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageBuilder.Domain.Entities;

namespace PageBuilder.Infrastructure.Data.Configurations;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToTable("Blocks");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Type).IsRequired().HasMaxLength(50);

        builder.Property(b => b.Order).IsRequired();

        // JSON column for Content (EF Core 7+)
        builder
            .Property(b => b.Content)
            .HasColumnType("json") // MySQL/SQL Server
            .IsRequired();

        // Relationships
        builder
            .HasOne(b => b.Page)
            .WithMany(p => p.Blocks)
            .HasForeignKey(b => b.PageId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(b => new { b.PageId, b.Order });
        // Prevent duplicate orders on the same page
        builder.HasIndex(b => new { b.PageId, b.Order }).IsUnique();
    }
}
