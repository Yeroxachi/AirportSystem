using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.DbConstants;

namespace Persistence.EntityConfiguration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.OriginFlights)
            .WithOne(x => x.Origin)
            .HasForeignKey(x => x.OriginId);
        builder.HasMany(x => x.DestinationFlights)
            .WithOne(x => x.Destination)
            .HasForeignKey(x => x.DestinationId);
        builder.Property(x => x.Name)
            .HasMaxLength(ConfigurationConstants.MaxCityNameLength);
    }
}