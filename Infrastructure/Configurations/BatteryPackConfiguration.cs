using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

internal class BatteryPackConfiguration : IEntityTypeConfiguration<BatteryPack>
{
    public void Configure(EntityTypeBuilder<BatteryPack> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.Property(x => x.NumberOfModulesConnectedInSeries).IsRequired().HasColumnType("integer").HasColumnName("modules_connected_series");
        builder.Property(x => x.NumberOfModulesConnectedInParallel).IsRequired().HasColumnType("integer").HasColumnName("modules_connected_parallel");

        builder.Property(x => x.Current).HasConversion<MeasureandQuantityConverter>().HasColumnName("current");
        builder.Property(x => x.Voltage).HasConversion<MeasureandQuantityConverter>().HasColumnName("voltage");
        builder.Property(x => x.Capacity).HasConversion<MeasureandQuantityConverter>().HasColumnName("capacity");
        builder.Property(x => x.Weight).HasConversion<MeasureandQuantityConverter>().HasColumnName("weight");
        builder.Property(x => x.MiscellaneousWeight).HasConversion<MeasureandQuantityConverter>().HasColumnName("miscellaneous_weight");
        builder.Property(x => x.Power).HasConversion<MeasureandQuantityConverter>().HasColumnName("power");

        builder.HasOne<BatteryModule>().WithMany().HasForeignKey(a => a.ModuleId).IsRequired();
    }
}