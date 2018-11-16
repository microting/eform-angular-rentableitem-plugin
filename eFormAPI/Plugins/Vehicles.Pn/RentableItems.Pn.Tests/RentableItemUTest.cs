using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentableItems.Pn.Tests
{
    [TestFixture]
    public class RentableItemUTest :DbTestFixture
    {

        [Test]
        public void RentableItemModel_Save_DoesSave()
        {

            // Arrange
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = Guid.NewGuid().ToString();
            rentableItemModel.ModelName = Guid.NewGuid().ToString();
            rentableItemModel.PlateNumber = Guid.NewGuid().ToString();
            rentableItemModel.VinNumber = Guid.NewGuid().ToString();
            rentableItemModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;
            rentableItemModel.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItemModel.RegistrationDate = DateTime.UtcNow; 

            // Act
            rentableItemModel.Save(DbContext);

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
            Assert.AreEqual(rentableItemModel.RegistrationDate, rentableItem.RegistrationDate);
            Assert.AreEqual(rentableItemModel.WorkflowState, rentableItem.Workflow_state);
            //rentableItemsPnDbAnySql.RentableItem.
        }

        [Test]
        public void RentableItemModel_Update_DoesUpdate()
        {
            // Arrange
            RentableItem rentableItem = new RentableItem();
            rentableItem.Brand = Guid.NewGuid().ToString();
            rentableItem.ModelName = Guid.NewGuid().ToString();
            rentableItem.PlateNumber = Guid.NewGuid().ToString();
            rentableItem.VinNumber = Guid.NewGuid().ToString();
            rentableItem.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItem.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItem.RegistrationDate = registrationDate;

            DbContext.RentableItem.Add(rentableItem);         
            DbContext.SaveChanges();

            RentableItemsVersions rentableItemVer = new RentableItemsVersions();

            rentableItemVer.Brand = rentableItem.Brand;
            rentableItemVer.Created_at = rentableItem.Created_at;
            rentableItemVer.Created_By_User_Id = rentableItem.Created_By_User_Id;
            rentableItemVer.ModelName = rentableItem.ModelName;
            rentableItemVer.PlateNumber = rentableItem.PlateNumber;
            rentableItemVer.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemVer.SerialNumber = rentableItem.SerialNumber;
            rentableItemVer.Updated_at = rentableItem.Updated_at;
            rentableItemVer.Updated_By_User_Id = rentableItem.Updated_By_User_Id;
            rentableItemVer.Version = rentableItem.Version;
            rentableItemVer.VinNumber = rentableItem.VinNumber;
            rentableItemVer.Workflow_state = rentableItem.Workflow_state;

            rentableItemVer.RentableItemId = rentableItem.Id;

            DbContext.RentableItemsVersion.Add(rentableItemVer);
            DbContext.SaveChanges();
        

            // Act
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = rentableItem.Brand;
            rentableItemModel.CreatedAt = rentableItem.Created_at;
            rentableItemModel.CreatedByUserId = rentableItem.Created_By_User_Id;
            rentableItemModel.ModelName = rentableItem.ModelName;
            rentableItemModel.PlateNumber = rentableItem.PlateNumber;
            rentableItemModel.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemModel.SerialNumber = rentableItem.SerialNumber;
            rentableItemModel.UpdatedAt = rentableItem.Updated_at;
            rentableItemModel.UpdatedByUserId = rentableItem.Updated_By_User_Id;
            rentableItemModel.VinNumber = "656565F";
            rentableItemModel.WorkflowState = rentableItem.Workflow_state;

            rentableItemModel.Update(DbContext);

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
            Assert.AreEqual(rentableItem.RegistrationDate, dbRentableItem.RegistrationDate);
            Assert.AreEqual(rentableItem.Workflow_state, dbRentableItem.Workflow_state);
        }

        [Test]
        public void RentableItemModel_Delete_DoesDelete()
        {
            // Arrange
            RentableItem rentableItem = new RentableItem();
            rentableItem.Brand = Guid.NewGuid().ToString();
            rentableItem.ModelName = Guid.NewGuid().ToString();
            rentableItem.PlateNumber = Guid.NewGuid().ToString();
            rentableItem.VinNumber = Guid.NewGuid().ToString();
            rentableItem.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItem.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItem.RegistrationDate = registrationDate;

            DbContext.RentableItem.Add(rentableItem);
            DbContext.SaveChanges();

            // Act
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = rentableItem.Brand;
            rentableItemModel.CreatedAt = rentableItem.Created_at;
            rentableItemModel.CreatedByUserId = rentableItem.Created_By_User_Id;
            rentableItemModel.ModelName = rentableItem.ModelName;
            rentableItemModel.PlateNumber = rentableItem.PlateNumber;
            rentableItemModel.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemModel.SerialNumber = rentableItem.SerialNumber;
            rentableItemModel.UpdatedAt = rentableItem.Updated_at;
            rentableItemModel.UpdatedByUserId = rentableItem.Updated_By_User_Id;
            rentableItemModel.VinNumber = rentableItem.VinNumber;
            rentableItemModel.WorkflowState = rentableItem.Workflow_state;

            RentableItemsVersions rentableItemVer = new RentableItemsVersions();

            rentableItemVer.Brand = rentableItem.Brand;
            rentableItemVer.Created_at = rentableItem.Created_at;
            rentableItemVer.Created_By_User_Id = rentableItem.Created_By_User_Id;
            rentableItemVer.ModelName = rentableItem.ModelName;
            rentableItemVer.PlateNumber = rentableItem.PlateNumber;
            rentableItemVer.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemVer.SerialNumber = rentableItem.SerialNumber;
            rentableItemVer.Updated_at = rentableItem.Updated_at;
            rentableItemVer.Updated_By_User_Id = rentableItem.Updated_By_User_Id;
            rentableItemVer.Version = rentableItem.Version;
            rentableItemVer.VinNumber = rentableItem.VinNumber;
            rentableItemVer.Workflow_state = rentableItem.Workflow_state;

            rentableItemVer.RentableItemId = rentableItem.Id;

            DbContext.RentableItemsVersion.Add(rentableItemVer);
            DbContext.SaveChanges();

            rentableItemModel.Delete(DbContext);

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
            Assert.AreEqual(rentableItem.RegistrationDate, dbRentableItem.RegistrationDate);
            Assert.AreEqual(eFormShared.Constants.WorkflowStates.Removed, dbRentableItem.Workflow_state);
        }
    }
}
