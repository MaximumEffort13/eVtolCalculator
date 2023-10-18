using Domain.Entities.DetailedDesign;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class MotorConfiguration : IEntityTypeConfiguration<Motor>
{
    public void Configure(EntityTypeBuilder<Motor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(25);

        builder.Property(x => x.VoltageRating).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("voltage_rating");
        builder.Property(x => x.CurrentRating).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("current_rating");
        builder.Property(x => x.Weight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("total_weight");


        builder.Property(x => x.Kv).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("Kv");
        builder.Property(x => x.PowerToWeightRatio).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("power_to_weight");

        builder.Property(x => x.Rpm).HasConversion<MeasureandQuantityConverter>().HasMaxLength(15).HasColumnName("maximum_rpm");
    }
}
