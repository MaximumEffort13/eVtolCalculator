using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ConceptDesign;
using Infrastructure.Persistence.Converters;

namespace Infrastructure.Persistence.Configurations;
internal class ConceptualDesignConfiguration : IEntityTypeConfiguration<ConceptualVtolDesign>
{
    public void Configure(EntityTypeBuilder<ConceptualVtolDesign> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).IsRequired();

        builder.Property(d => d.TotalDesignWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("design_weight");

        builder.Property(d => d.PayloadWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("payload_weight");

        builder.Property(d => d.FlightTimeRequirementInMinutes).HasConversion(
            t => t.TotalMinutes,
            value => TimeSpan.FromMinutes(value));

        builder.Property(d => d.PowerRequirement).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("power_required");

        builder.Property(d => d.BatteryCapacityRequirement).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("battery_capacity");

        builder.Property(d => d.BatteryWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("battery_weight");

        builder.Property(d => d.MotorWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("motor_weight");

        builder.Property(d => d.Horsepower).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("horsepower");

    }
}