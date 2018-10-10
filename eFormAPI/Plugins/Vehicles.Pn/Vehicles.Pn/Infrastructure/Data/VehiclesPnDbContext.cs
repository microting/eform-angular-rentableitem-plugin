using Microsoft.EntityFrameworkCore;
using Vehicles.Pn.Infrastructure.Data.Entities;

namespace Vehicles.Pn.Infrastructure.Data
{
    public class VehiclesPnDbContext : DbContext
    {
        public VehiclesPnDbContext(DbContextOptions<VehiclesPnDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleInspection> VehicleInspections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .HasIndex(x => x.VinNumber)
                .IsUnique();
        }
    }
}