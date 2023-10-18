using Domain.Entities.ConceptDesign;
using Domain.Entities.DetailedDesign;
using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
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
            builder.Property(e => e.FlightTimeInMinutes).HasConversion(conv => conv.TotalMinutes, value => TimeSpan.FromMinutes(value)).IsRequired();
            builder.Property(e => e.DiscLoading).HasConversion<MeasureandQuantityConverter>().IsRequired().HasMaxLength(50);
            builder.Property(e => e.Thrust).HasConversion<MeasureandQuantityConverter>().IsRequired().HasMaxLength(50);
            builder.Property(e => e.ThrustArea).HasConversion<MeasureandQuantityConverter>().IsRequired().HasMaxLength(50);
            builder.Property(e => e.PayloadWeight).HasConversion<MeasureandQuantityConverter>().IsRequired().HasMaxLength(50);
            builder.Property(e => e.LiftOffWeight).HasConversion<MeasureandQuantityConverter>().IsRequired().HasMaxLength(50);
            builder.Property(e => e.PowerLoading).HasConversion<MeasureandQuantityConverter>().IsRequired().HasMaxLength(50);

            builder.HasOne<MissionParameterEstimates>().WithMany().HasForeignKey(a => a.MissionParameterId).IsRequired();
            builder.HasOne<BatteryPack>().WithMany().HasForeignKey(a => a.BatteryPackId).IsRequired();
            builder.HasOne<Fuselage>().WithMany().HasForeignKey(a => a.FuselageId).IsRequired();
            builder.HasOne<Motor>().WithMany().HasForeignKey(a => a.MotorId).IsRequired();
            builder.HasOne<Inverter>().WithMany().HasForeignKey(a => a.InverterId).IsRequired();
            builder.HasOne<Blade>().WithMany().HasForeignKey(a => a.BladeId).IsRequired();
        }
    }
}
