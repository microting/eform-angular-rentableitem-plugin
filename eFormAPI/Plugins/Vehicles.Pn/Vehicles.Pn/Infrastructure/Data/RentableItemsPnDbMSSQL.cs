namespace RentableItems.Pn.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using RentableItems.Pn.Infrastructure.Data.Entities;

    public partial class RentableItemsPnDbMSSQL : DbContext, MicrotingContextInterface
    {
        public RentableItemsPnDbMSSQL() { }

        public RentableItemsPnDbMSSQL(DbContextOptions options)
          : base(options)
        {
        }
        public virtual Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade ContextDatabase
        {
            get => base.Database;
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

        // Uncomment the below section, when creating migrations
        // Add-Migration -Context RentableItemsPnDbMSSQL -OutputDir Infrastructure\Migrations\MSSQL
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"data source=(LocalDb)\SharedInstance;Initial catalog=rentableitems;Integrated Security=True");
        //    }
        //}
    }
}