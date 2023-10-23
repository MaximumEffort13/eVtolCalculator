using Domain.Entities.AuthenticationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();
        builder.Property(a => a.PersonId).IsRequired();

        builder.Property(p => p.StreetName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Suburb).IsRequired().HasMaxLength(50);
        builder.Property(p => p.City).HasMaxLength(50);

        builder.Property(p => p.Province).IsRequired().HasMaxLength(50);
        builder.Property(p => p.PostalCode).IsRequired().HasMaxLength(50);
    }
}
