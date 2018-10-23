namespace RentableItems.Pn.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using RentableItems.Pn.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Storage;
    using MySql.Data.MySqlClient;
    using System;

    public partial class RentableItemsPnDbAnySql : DbContext
    {
        public RentableItemsPnDbAnySql() { }

        public RentableItemsPnDbAnySql(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractVersions> ContractVersions { get; set; }
        public virtual DbSet<ContractInspection> ContractInspection { get; set; }
        public virtual DbSet<ContractInspectionVersion> ContractInspectionVersion { get; set; }
        public virtual DbSet<RentableItem> RentableItem { get; set; }
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
        }
    }
}
