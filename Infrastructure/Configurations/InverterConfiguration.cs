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

internal class InverterConfiguration : IEntityTypeConfiguration<Inverter>
{
    public void Configure(EntityTypeBuilder<Inverter> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        builder.Property(x => x.VoltageRating).HasConversion<MeasureandQuantity>().HasColumnName("voltage_rating");
        builder.Property(x => x.CurrentRating).HasConversion<MeasureandQuantity>().HasColumnName("current_rating");
        builder.Property(x => x.Weight).HasConversion<MeasureandQuantity>().HasColumnName("total_weight");
        builder.Property(x => x.PowerToWeightRatio).HasConversion<MeasureandQuantity>().HasColumnName("power_to_weight");

    }
}
