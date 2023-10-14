using Domain.Entities.ConceptDesign;
using Domain.Entities.DetailedDesign;
using Domain.Entities.DetailedDesign.Battery;
using Domain.Primitives;
using Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    internal class ElectricVtolDesignConfiguration : IEntityTypeConfiguration<ElectricVtolDesign>
    {
        public void Configure(EntityTypeBuilder<ElectricVtolDesign> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired();

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.MotorQuantity).IsRequired().HasColumnType("integer");
            builder.Property(e => e.BladePerMotorQuantity).IsRequired().HasColumnType("integer");
            builder.Property(e => e.FlightTimeInMinutes).HasConversion(conv => conv.TotalMinutes, value => TimeSpan.FromMinutes(value)).IsRequired().HasColumnType("double");
            builder.Property(e => e.DiscLoading).HasConversion<MeasureandQuantityConverter>().IsRequired().HasColumnType("string");
            builder.Property(e => e.Thrust).HasConversion<MeasureandQuantityConverter>().IsRequired().HasColumnType("string");
            builder.Property(e => e.ThrustArea).HasConversion<MeasureandQuantityConverter>().IsRequired().HasColumnType("string");
            builder.Property(e => e.PayloadWeight).HasConversion<MeasureandQuantityConverter>().IsRequired().HasColumnType("string");
            builder.Property(e => e.LiftOffWeight).HasConversion<MeasureandQuantityConverter>().IsRequired().HasColumnType("string");
            builder.Property(e => e.PowerLoading).HasConversion<MeasureandQuantityConverter>().IsRequired().HasColumnType("string");

            builder.HasOne<MissionParameterEstimates>().WithMany().HasForeignKey(a => a.MissionParameterId).IsRequired();
            builder.HasOne<BatteryPack>().WithMany().HasForeignKey(a => a.BatteryPackId).IsRequired();
            builder.HasOne<Fuselage>().WithMany().HasForeignKey(a => a.FuselageId).IsRequired();
            builder.HasOne<Motor>().WithMany().HasForeignKey(a => a.MotorId).IsRequired();
            builder.HasOne<Inverter>().WithMany().HasForeignKey(a => a.InverterId).IsRequired();
            builder.HasOne<Blade>().WithMany().HasForeignKey(a => a.BladeId).IsRequired();
        }
    }
}
