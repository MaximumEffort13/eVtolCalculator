using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Converters;
using Domain.Entities.ConceptDesign;

namespace Infrastructure.Configurations;
internal class MissionParametersConfiguration : IEntityTypeConfiguration<MissionParameterEstimates>
{
    public void Configure(EntityTypeBuilder<MissionParameterEstimates> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).IsRequired();

        builder.Property(d => d.TotalDesignWeight).HasConversion<MeasureandQuantityConverter>().HasColumnName("calculated_weight");

        builder.Property(d => d.PayloadWeight).HasConversion<MeasureandQuantityConverter>().HasColumnName("payload_weight");

        builder.Property(d => d.FlightTimeRequirementInMinutes).HasConversion(
            t => t.TotalMinutes,
            value => TimeSpan.FromMinutes(value));

        builder.Property(d => d.EstimatedPowerRequirement).HasConversion<MeasureandQuantityConverter>().HasColumnName("calculated_power_required");

        builder.Property(d => d.EstimatedBatteryCapacityRequirement).HasConversion<MeasureandQuantityConverter>().HasColumnName("calculated_battery_capacity");

        builder.Property(d => d.EstimatedBatteryWeight).HasConversion<MeasureandQuantityConverter>().HasColumnName("calculated_battery_weight");

        builder.Property(d => d.EstimatedMotorWeight).HasConversion<MeasureandQuantityConverter>().HasColumnName("calculated_motor_weight");

        builder.Property(d => d.EstimatedHorsepowerRequiredForHover).HasConversion<MeasureandQuantityConverter>().HasColumnName("Calculated_horsepower_required");

    }
}