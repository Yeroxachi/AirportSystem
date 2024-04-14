using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfiguration;

namespace Persistence.Context;

public class AirportContext : DbContext
{
    public AirportContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;
    public DbSet<Flight> Flights { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AirportContext).Assembly);
    }
}