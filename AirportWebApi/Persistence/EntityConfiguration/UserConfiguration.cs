using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.DbConstants;

namespace Persistence.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Flights)
            .WithMany(x => x.Clients);
        builder.Property(x => x.Username).HasMaxLength(ConfigurationConstants.MaxUserNameLength);
    }
}