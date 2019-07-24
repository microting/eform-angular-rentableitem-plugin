﻿using Microsoft.EntityFrameworkCore;
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
    public class RentableItemSettingsUTest : DbTestFixture
    {
        [Test]
        public async Task RentableItemSettingsModel_Save_DoesSave()
        {
            // Arrange
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            Random rnd = new Random();
            rentableItemsSettingsModel.CreatedByUserID = rnd.Next(1, 541);
            rentableItemsSettingsModel.UpdatedByUserID = rnd.Next(1, 541);
            rentableItemsSettingsModel.eFormId = rnd.Next(123, 582);

            // Act
            await rentableItemsSettingsModel.Create(DbContext);

            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(1, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.Created_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.Updated_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.eFormId, dbRentableItemsSettings.eForm_Id);

        }
        [Test]
        public async Task RentableItemSettingsModel_Update_DoesUpdate()
        {
            // Arrange
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();
            Random rnd = new Random();
            rentableItemsSettings.Created_By_User_Id = rnd.Next(1, 82);
            rentableItemsSettings.eForm_Id = rnd.Next(5, 100);
            rentableItemsSettings.Updated_By_User_Id = rnd.Next(1, 100);

            DbContext.RentableItemsSettings.Add(rentableItemsSettings);
            await DbContext.SaveChangesAsync();

            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();
            rentableItemsSettingsVer.Created_By_User_Id = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsVer.Eform_Id = rentableItemsSettings.eForm_Id.Value;
            rentableItemsSettingsVer.Updated_By_User_Id = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            DbContext.RentableItemsSettingsVersions.Add(rentableItemsSettingsVer);
            await DbContext.SaveChangesAsync();
            // Act
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            rentableItemsSettingsModel.CreatedByUserID = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsModel.eFormId = 555;
            rentableItemsSettingsModel.UpdatedByUserID = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsModel.Id = rentableItemsSettings.Id;
            await rentableItemsSettingsModel.Update(DbContext);
            
            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(2, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.Created_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.Updated_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.eFormId, dbRentableItemsSettings.eForm_Id);


        }
        [Test]
        public async Task RentableItemSettingsModel_Delete_DoesDelete()
        {
            // Arrange
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();
            Random rnd = new Random();
            rentableItemsSettings.Created_By_User_Id = rnd.Next(1, 82);
            rentableItemsSettings.eForm_Id = rnd.Next(5, 100);
            rentableItemsSettings.Updated_By_User_Id = rnd.Next(1, 100);

            DbContext.RentableItemsSettings.Add(rentableItemsSettings);
            await DbContext.SaveChangesAsync();

            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();
            rentableItemsSettingsVer.Created_By_User_Id = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsVer.Eform_Id = rentableItemsSettings.eForm_Id.Value;
            rentableItemsSettingsVer.Updated_By_User_Id = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            DbContext.RentableItemsSettingsVersions.Add(rentableItemsSettingsVer);
            await DbContext.SaveChangesAsync();
            // Act
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            rentableItemsSettingsModel.CreatedByUserID = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsModel.eFormId = rentableItemsSettings.eForm_Id;
            rentableItemsSettingsModel.UpdatedByUserID = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsModel.Id = rentableItemsSettings.Id;

            await rentableItemsSettingsModel.Delete(DbContext);

            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(2, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.Created_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.Updated_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.eFormId, dbRentableItemsSettings.eForm_Id);
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbRentableItemsSettings.Workflow_state);


        }
    }
}
