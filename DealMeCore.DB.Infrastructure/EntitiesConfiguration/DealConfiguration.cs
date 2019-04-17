using DealMeCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DealMeCore.DB.Infrastructure.EntitiesConfiguration
{
    internal class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            // Table name
            builder.ToTable("Deals");

            // Primary key
            builder
                .HasKey(e => e.Id)
                .HasName("PK_DEALS");

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
                .Property(e => e.IsActive)
                .IsRequired();

            builder
                .Property(e => e.DealValidFrom)
                .IsRequired();

            builder
                .Property(e => e.DealValidTo)
                .IsRequired();

            builder
                .Property(e => e.MarketUrl)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(e => e.BuyNowUrl)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(e => e.DealType)
                .IsRequired();

            builder
                .Property(e => e.BrandId)
                .IsRequired();

            // One-to-Many /Deals - Images/
            builder
                .HasMany(e => e.Images)
                .WithOne()
                .HasForeignKey(e => e.DealId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
