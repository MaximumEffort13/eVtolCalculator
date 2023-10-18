using Domain.Entities.DetailedDesign;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class BladeConfiguration : IEntityTypeConfiguration<Blade>
{
    public void Configure(EntityTypeBuilder<Blade> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.Property(x => x.Weight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("total_weight");
        builder.Property(x => x.Length).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("length");
        builder.Property(x => x.Width).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("width");
        builder.Property(x => x.Thickness).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("thickness");
        builder.Property(x => x.AngleOfAttack).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("angle_attack");
    }
}
