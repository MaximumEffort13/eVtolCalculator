using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ConceptDesign;
using Infrastructure.Persistence.Converters;

namespace Infrastructure.Persistence.Configurations;
internal class MissionParametersConfiguration : IEntityTypeConfiguration<MissionParameterEstimates>
{
    public void Configure(EntityTypeBuilder<MissionParameterEstimates> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).IsRequired();

        builder.Property(d => d.TotalDesignWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("calculated_weight");

        builder.Property(d => d.PayloadWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("payload_weight");

        builder.Property(d => d.FlightTimeRequirementInMinutes).HasConversion(
            t => t.TotalMinutes,
            value => TimeSpan.FromMinutes(value));

        builder.Property(d => d.EstimatedPowerRequirement).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("calculated_power_required");

        builder.Property(d => d.EstimatedBatteryCapacityRequirement).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("calculated_battery_capacity");

        builder.Property(d => d.EstimatedBatteryWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("calculated_battery_weight");

        builder.Property(d => d.EstimatedMotorWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("calculated_motor_weight");

        builder.Property(d => d.EstimatedHorsepowerRequiredForHover).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("Calculated_horsepower_required");

    }
}