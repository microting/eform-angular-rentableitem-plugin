using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Vehicles.Pn.Infrastructure.Data.Entities;

namespace Vehicles.Pn.Infrastructure.Data
{
    public class VehiclesPnDbContext : DbContext
    {
        public VehiclesPnDbContext()
            : base("eFormVehiclesPnConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<VehiclesPnDbContext>(null);
        }

        public VehiclesPnDbContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<VehiclesPnDbContext>(null);
        }

        public static VehiclesPnDbContext Create()
        {
            return new VehiclesPnDbContext();
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleContract> VehicleContracts { get; set; }
        public DbSet<VehicleInspection> VehicleInspections { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.VinNumber)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}
