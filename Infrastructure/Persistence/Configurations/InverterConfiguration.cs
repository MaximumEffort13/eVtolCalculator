using Domain.Entities.DetailedDesign;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence.Configurations;

internal class InverterConfiguration : IEntityTypeConfiguration<Inverter>
{
    public void Configure(EntityTypeBuilder<Inverter> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.Property(x => x.VoltageRating).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("voltage_rating");
        builder.Property(x => x.CurrentRating).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("current_rating");
        builder.Property(x => x.Weight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("total_weight");
        builder.Property(x => x.PowerToWeightRatio).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("power_to_weight");

    }
}
