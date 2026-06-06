using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class BatteryModuleConfiguration : IEntityTypeConfiguration<BatteryModule>
{
    public void Configure(EntityTypeBuilder<BatteryModule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.NumberOfCellsConnectedInSeries).IsRequired().HasColumnType("integer").HasColumnName("cellc_connected_series");
        builder.Property(x => x.NumberOfCellsConnectedInParallel).IsRequired().HasColumnType("integer").HasColumnName("cellc_connected_parallel");

        builder.Property(x => x.Energy).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("energy");
        builder.Property(x => x.Voltage).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("voltage");
        builder.Property(x => x.Capacity).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("capacity");
        builder.Property(x => x.Mass).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("mass");

        builder.HasOne<Cell>().WithMany().HasForeignKey(a => a.CellId).IsRequired();
    }
}
