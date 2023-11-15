using Domain.Entities.DetailedDesign.Battery;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Converters;

namespace Infrastructure.Persistence.Configurations;

internal class BatteryPackConfiguration : IEntityTypeConfiguration<BatteryPack>
{
    public void Configure(EntityTypeBuilder<BatteryPack> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.Property(x => x.NumberOfModulesConnectedInSeries).IsRequired().HasColumnType("integer").HasColumnName("modules_connected_series");
        builder.Property(x => x.NumberOfModulesConnectedInParallel).IsRequired().HasColumnType("integer").HasColumnName("modules_connected_parallel");

        builder.Property(x => x.Voltage).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("voltage");
        builder.Property(x => x.Capacity).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("capacity");
        builder.Property(x => x.Mass).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("mass");
        builder.Property(x => x.MiscellaneousWeight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("miscellaneous_weight");
        builder.Property(x => x.Energy).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("energy");
        builder.Property(x => x.SpecificEnergy).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("specific_energy");

        builder.HasOne<BatteryModule>().WithMany().HasForeignKey(a => a.ModuleId).IsRequired();
    }
}