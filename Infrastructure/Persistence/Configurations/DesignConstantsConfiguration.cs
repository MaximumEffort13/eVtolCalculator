using Domain.Entities;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class DesignConstantsConfiguration : IEntityTypeConfiguration<DesignConstantsEntity>
{
    public void Configure(EntityTypeBuilder<DesignConstantsEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Value).HasColumnName("constant_value");
    }
}
