using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RentableItems.Pn.Infrastructure.Data.Entities;
using System;

namespace RentableItems.Pn.Infrastructure.Data
{
    public interface MicrotingContextInterface : IDisposable
    {
        DbSet<Contract> Contract { get; set; }
        DbSet<ContractVersions> ContractVersions { get; set; }
        DbSet<ContractInspection> ContractInspection { get; set; }
        DbSet<ContractInspectionVersion> ContractInspectionVersion { get; set; }
        DbSet<Field> Fields { get; set; }
        DbSet<RentableItem> RentableItem { get; set; }
        DbSet<RentableItemsField> RentableItemsFields { get; set; }
        DbSet<RentableItemsVersions> RentableItemsVersion { get; set; }
        DbSet<RentableItemContract> RentableItemContract { get; set; }
        DbSet<RentableItemsContractVersions> RentableItemsContractVersions { get; set; }
        DbSet<RentableItemsSettings> RentableItemsSettings { get; set; }
        DbSet<RentableItemsSettingsVersions> RentableItemsSettingsVersions { get; set; }

        int SaveChanges();
        Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade ContextDatabase
        {
            get;
        }
    }
}