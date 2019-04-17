using DealMeCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DealMeCore.DB.Infrastructure.EntitiesConfiguration
{
    internal class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            // Table name
            builder.ToTable("Stores");

            // Primary key
            builder
                .HasKey(e => e.Id)
                .HasName("PK_STORES");

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
                .Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(400);

            // Indexes
            builder
                .HasIndex(e => new { e.Name })
                .HasName("IX_NAME")
                .IsUnique(false);

        }
    }
}
