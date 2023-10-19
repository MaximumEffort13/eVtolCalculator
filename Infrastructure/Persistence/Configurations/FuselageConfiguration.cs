using Domain.Entities.DetailedDesign;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class FuselageConfiguration : IEntityTypeConfiguration<FuselageEntity>
    {
        public void Configure(EntityTypeBuilder<FuselageEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Weight).HasConversion<MeasureandQuantityConverter>().HasMaxLength(50).HasColumnName("weight");
        }
    }
}
