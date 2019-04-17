using DealMeCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DealMeCore.DB.Infrastructure.EntitiesConfiguration
{
    internal class DealImageConfiguration : IEntityTypeConfiguration<DealImage>
    {
        public void Configure(EntityTypeBuilder<DealImage> builder)
        {
            // Table name
            builder.ToTable("DealImages");

            // Primary key
            builder
                .HasKey(e => e.Id)
                .HasName("PK_DEALIMAGES");

            builder
                .Property(e => e.Id)
                .IsRequired()
                .HasValueGenerator<SequentialGuidValueGenerator>();

            // Properties
            builder
                .Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(400);

        }
    }
}
