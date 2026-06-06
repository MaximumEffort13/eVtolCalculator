using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class BatteryCellConfiguration : IEntityTypeConfiguration<Cell>
{
    public void Configure(EntityTypeBuilder<Cell> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Energy).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("energy");
        builder.Property(x => x.Voltage).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("voltage");
        builder.Property(x => x.Capacity).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("capacity");
        builder.Property(x => x.CellMass).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("mass");
    }
}
