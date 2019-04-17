using DealMeCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DealMeCore.DB.Infrastructure.EntitiesConfiguration
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            // Table name
            builder.ToTable("Brands");

            // Primary key
            builder
                .HasKey(e => e.Id)
                .HasName("PK_BRANDS");

            builder
                .Property(e => e.Id)
                .IsRequired()
                .HasValueGenerator<SequentialGuidValueGenerator>();

            // Properties
            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder
                .Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(250);

            // Indexes
            builder
                .HasIndex(e => new { e.Name })
                .HasName("IX_NAME")
                .IsUnique(false);

            // One-to-Many /Brand - Deals/
            builder
                .HasMany(e => e.Deals)
                .WithOne()
                .HasForeignKey(e => e.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many /Brand - Stores/
            builder
                .HasMany(e => e.Stores)
                .WithOne()
                .HasForeignKey(e => e.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
