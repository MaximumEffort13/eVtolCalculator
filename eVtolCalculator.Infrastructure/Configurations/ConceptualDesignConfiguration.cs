using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Converters;
using Domain.Entities.ConceptDesign;

namespace Infrastructure.Configurations;
internal class ConceptualDesignConfiguration : IEntityTypeConfiguration<MissionParameterEstimates>
{
    public void Configure(EntityTypeBuilder<MissionParameterEstimates> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).IsRequired();

        builder.Property(d => d.TotalDesignWeight).HasConversion<MeasurementQuantityConverter>().HasColumnName("design_weight");

        builder.Property(d => d.PayloadWeight).HasConversion<MeasurementQuantityConverter>().HasColumnName("payload_weight");

        builder.Property(d => d.FlightTimeRequirementInMinutes).HasConversion(
            t => t.TotalMinutes,
            value => TimeSpan.FromMinutes(value));

        builder.Property(d => d.EstimatedPowerRequirement).HasConversion<MeasurementQuantityConverter>().HasColumnName("power_required");

        builder.Property(d => d.EstimatedBatteryCapacityRequirement).HasConversion<MeasurementQuantityConverter>().HasColumnName("battery_capacity");

        builder.Property(d => d.EstimatedBatteryWeight).HasConversion<MeasurementQuantityConverter>().HasColumnName("battery_weight");

        builder.Property(d => d.EstimatedMotorWeight).HasConversion<MeasurementQuantityConverter>().HasColumnName("motor_weight");

        builder.Property(d => d.EstimatedHorsepowerRequiredForHover).HasConversion<MeasurementQuantityConverter>().HasColumnName("horsepower");

    }
}