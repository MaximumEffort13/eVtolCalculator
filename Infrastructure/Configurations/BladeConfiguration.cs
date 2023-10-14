using Domain.Entities.DetailedDesign;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

internal class BladeConfiguration : IEntityTypeConfiguration<Blade>
{
    public void Configure(EntityTypeBuilder<Blade> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.Property(x => x.Weight).HasConversion<MeasureandQuantity>().HasColumnName("total_weight");
        builder.Property(x => x.AngleOfAttack).HasConversion<MeasureandQuantity>().HasColumnName("angle_attack");
        builder.Property(x => x.Length).HasConversion<MeasureandQuantity>().HasColumnName("length");
        builder.Property(x => x.Weight).HasConversion<MeasureandQuantity>().HasColumnName("total_weight");
        builder.Property(x => x.Thickness).HasConversion<MeasureandQuantity>().HasColumnName("thickness");
    }
}
