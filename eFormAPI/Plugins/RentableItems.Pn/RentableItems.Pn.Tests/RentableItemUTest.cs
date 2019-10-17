using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microting.eForm.Infrastructure.Constants;

namespace RentableItems.Pn.Tests
{
    [TestFixture]
    public class RentableItemUTest : DbTestFixture
    {

        [Test]
        public async Task RentableItemModel_Save_DoesSave()
        {

            // Arrange
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = Guid.NewGuid().ToString();
            rentableItemModel.ModelName = Guid.NewGuid().ToString();
            rentableItemModel.PlateNumber = Guid.NewGuid().ToString();
            rentableItemModel.VinNumber = Guid.NewGuid().ToString();
            rentableItemModel.WorkflowState = Constants.WorkflowStates.Created;
            rentableItemModel.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItemModel.RegistrationDate = registrationDate; 


            // Act
            await rentableItemModel.Create(DbContext);

            RentableItem rentableItem = DbContext.RentableItem.AsNoTracking().First();
            List<RentableItem> itemList = DbContext.RentableItem.AsNoTracking().ToList();
            List<RentableItemsVersions> versionList = DbContext.RentableItemsVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(rentableItem);

            Assert.AreEqual(1, itemList.Count());

            Assert.AreEqual(1, versionList.Count());

            Assert.AreEqual(rentableItemModel.Brand, rentableItem.Brand);
            Assert.AreEqual(rentableItemModel.ModelName, rentableItem.ModelName);
            Assert.AreEqual(rentableItemModel.PlateNumber, rentableItem.PlateNumber);
            Assert.AreEqual(rentableItemModel.VinNumber, rentableItem.VinNumber);
            Assert.AreEqual(rentableItemModel.SerialNumber, rentableItem.SerialNumber);
            Assert.AreEqual(rentableItemModel.RegistrationDate.ToString(), rentableItem.RegistrationDate.ToString());
            Assert.AreEqual(rentableItemModel.WorkflowState, rentableItem.WorkflowState);
            //rentableItemsPnDbAnySql.RentableItem.
        }

        [Test]
        public async Task RentableItemModel_Update_DoesUpdate()
        {
            // Arrange
            RentableItem rentableItem = new RentableItem();
            rentableItem.Brand = Guid.NewGuid().ToString();
            rentableItem.ModelName = Guid.NewGuid().ToString();
            rentableItem.PlateNumber = Guid.NewGuid().ToString();
            rentableItem.VinNumber = Guid.NewGuid().ToString();
            rentableItem.WorkflowState = Constants.WorkflowStates.Created;
            rentableItem.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItem.RegistrationDate = registrationDate;

            DbContext.RentableItem.Add(rentableItem);         
            await DbContext.SaveChangesAsync();

            RentableItemsVersions rentableItemVer = new RentableItemsVersions();

            rentableItemVer.Brand = rentableItem.Brand;
            rentableItemVer.CreatedAt = rentableItem.CreatedAt;
            rentableItemVer.CreatedByUserId = rentableItem.CreatedByUserId;
            rentableItemVer.ModelName = rentableItem.ModelName;
            rentableItemVer.PlateNumber = rentableItem.PlateNumber;
            rentableItemVer.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemVer.SerialNumber = rentableItem.SerialNumber;
            rentableItemVer.UpdatedAt = rentableItem.UpdatedAt;
            rentableItemVer.UpdatedByUserId = rentableItem.UpdatedByUserId;
            rentableItemVer.Version = rentableItem.Version;
            rentableItemVer.VinNumber = rentableItem.VinNumber;
            rentableItemVer.WorkflowState = rentableItem.WorkflowState;

            rentableItemVer.RentableItemId = rentableItem.Id;

            DbContext.RentableItemsVersion.Add(rentableItemVer);
            await DbContext.SaveChangesAsync();
        

            // Act
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = rentableItem.Brand;
            rentableItemModel.CreatedAt = rentableItem.CreatedAt;
            rentableItemModel.CreatedByUserId = rentableItem.CreatedByUserId;
            rentableItemModel.ModelName = rentableItem.ModelName;
            rentableItemModel.PlateNumber = rentableItem.PlateNumber;
            rentableItemModel.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemModel.SerialNumber = rentableItem.SerialNumber;
            rentableItemModel.UpdatedAt = rentableItem.UpdatedAt;
            rentableItemModel.UpdatedByUserId = rentableItem.UpdatedByUserId;
            rentableItemModel.VinNumber = "656565F";
            rentableItemModel.WorkflowState = rentableItem.WorkflowState;

            rentableItemModel.Id = rentableItem.Id;

            await rentableItemModel.Update(DbContext);

            RentableItem dbRentableItem = DbContext.RentableItem.AsNoTracking().First();
            List<RentableItem> itemList = DbContext.RentableItem.AsNoTracking().ToList();
            List<RentableItemsVersions> versionList = DbContext.RentableItemsVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(rentableItem);

            Assert.AreEqual(1, itemList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(rentableItem.Brand, dbRentableItem.Brand);
            Assert.AreEqual(rentableItem.ModelName, dbRentableItem.ModelName);
            Assert.AreEqual(rentableItem.PlateNumber, dbRentableItem.PlateNumber);
            Assert.AreEqual(rentableItem.VinNumber, dbRentableItem.VinNumber);
            Assert.AreEqual(rentableItem.SerialNumber, dbRentableItem.SerialNumber);
            Assert.AreEqual(rentableItem.RegistrationDate.ToString(), dbRentableItem.RegistrationDate.ToString());
            Assert.AreEqual(rentableItem.WorkflowState, dbRentableItem.WorkflowState);
        }

        [Test]
        public async Task RentableItemModel_Delete_DoesDelete()
        {
            // Arrange
            RentableItem rentableItem = new RentableItem();
            rentableItem.Brand = Guid.NewGuid().ToString();
            rentableItem.ModelName = Guid.NewGuid().ToString();
            rentableItem.PlateNumber = Guid.NewGuid().ToString();
            rentableItem.VinNumber = Guid.NewGuid().ToString();
            rentableItem.WorkflowState = Constants.WorkflowStates.Created;
            rentableItem.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItem.RegistrationDate = registrationDate;

            DbContext.RentableItem.Add(rentableItem);
            await DbContext.SaveChangesAsync();

            // Act
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = rentableItem.Brand;
            rentableItemModel.CreatedAt = rentableItem.CreatedAt;
            rentableItemModel.CreatedByUserId = rentableItem.CreatedByUserId;
            rentableItemModel.ModelName = rentableItem.ModelName;
            rentableItemModel.PlateNumber = rentableItem.PlateNumber;
            rentableItemModel.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemModel.SerialNumber = rentableItem.SerialNumber;
            rentableItemModel.UpdatedAt = rentableItem.UpdatedAt;
            rentableItemModel.UpdatedByUserId = rentableItem.UpdatedByUserId;
            rentableItemModel.VinNumber = rentableItem.VinNumber;
            rentableItemModel.WorkflowState = rentableItem.WorkflowState;

            rentableItemModel.Id = rentableItem.Id;

            RentableItemsVersions rentableItemVer = new RentableItemsVersions();

            rentableItemVer.Brand = rentableItem.Brand;
            rentableItemVer.CreatedAt = rentableItem.CreatedAt;
            rentableItemVer.CreatedByUserId = rentableItem.CreatedByUserId;
            rentableItemVer.ModelName = rentableItem.ModelName;
            rentableItemVer.PlateNumber = rentableItem.PlateNumber;
            rentableItemVer.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemVer.SerialNumber = rentableItem.SerialNumber;
            rentableItemVer.UpdatedAt = rentableItem.UpdatedAt;
            rentableItemVer.UpdatedByUserId = rentableItem.UpdatedByUserId;
            rentableItemVer.Version = rentableItem.Version;
            rentableItemVer.VinNumber = rentableItem.VinNumber;
            rentableItemVer.WorkflowState = rentableItem.WorkflowState;

            rentableItemVer.RentableItemId = rentableItem.Id;

            DbContext.RentableItemsVersion.Add(rentableItemVer);
            await DbContext.SaveChangesAsync();

            await rentableItemModel.Delete(DbContext);

            RentableItem dbRentableItem = DbContext.RentableItem.AsNoTracking().First();
            List<RentableItem> itemList = DbContext.RentableItem.AsNoTracking().ToList();
            List<RentableItemsVersions> versionList = DbContext.RentableItemsVersion.AsNoTracking().ToList();
            //Assert
            
            Assert.NotNull(rentableItem);

            Assert.AreEqual(1, itemList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(rentableItem.Brand, dbRentableItem.Brand);
            Assert.AreEqual(rentableItem.ModelName, dbRentableItem.ModelName);
            Assert.AreEqual(rentableItem.PlateNumber, dbRentableItem.PlateNumber);
            Assert.AreEqual(rentableItem.VinNumber, dbRentableItem.VinNumber);
            Assert.AreEqual(rentableItem.SerialNumber, dbRentableItem.SerialNumber);
            Assert.AreEqual(rentableItem.RegistrationDate.ToString(), dbRentableItem.RegistrationDate.ToString());
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbRentableItem.WorkflowState);
        }
    }
}
