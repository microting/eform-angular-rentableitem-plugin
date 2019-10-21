using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using RentableItems.Pn.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Infrastructure.Data
{


    public partial class RentableItemsPnDbContext : DbContext, IPluginDbContext
    {
        public RentableItemsPnDbContext() { }

        public RentableItemsPnDbContext(DbContextOptions<RentableItemsPnDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractVersions> ContractVersions { get; set; }
        public virtual DbSet<ContractInspection> ContractInspection { get; set; }
        public virtual DbSet<ContractInspectionVersion> ContractInspectionVersion { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<RentableItem> RentableItem { get; set; }
        public virtual DbSet<RentableItemsField> RentableItemsFields { get; set; }
        public virtual DbSet<RentableItemsVersions> RentableItemsVersion { get; set; }
        public virtual DbSet<RentableItemContract> RentableItemContract { get; set; }
        public virtual DbSet<RentableItemsContractVersions> RentableItemsContractVersions { get; set; }
        public virtual DbSet<RentableItemsSettings> RentableItemsSettings { get; set; }
        public virtual DbSet<RentableItemsSettingsVersions> RentableItemsSettingsVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentableItem>()
                .HasIndex(x => x.VinNumber)
                .IsUnique();
            modelBuilder.Entity<Field>()
                 .HasIndex(x => x.Name);
        }
        
        public DbSet<PluginConfigurationValue> PluginConfigurationValues { get; set; }
        public DbSet<PluginConfigurationValueVersion> PluginConfigurationValueVersions { get; set; }
        public DbSet<PluginPermission> PluginPermissions { get; set; }
        public DbSet<PluginGroupPermission> PluginGroupPermissions { get; set; }
    }
}
