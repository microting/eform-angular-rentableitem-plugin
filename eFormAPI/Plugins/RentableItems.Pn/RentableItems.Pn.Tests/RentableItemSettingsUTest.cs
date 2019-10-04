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

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.CreatedByUserId);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.UpdatedByUserId);
            Assert.AreEqual(rentableItemsSettingsModel.eFormId, dbRentableItemsSettings.eFormId);

        }
        [Test]
        public async Task RentableItemSettingsModel_Update_DoesUpdate()
        {
            // Arrange
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();
            Random rnd = new Random();
            rentableItemsSettings.CreatedByUserId = rnd.Next(1, 82);
            rentableItemsSettings.eFormId = rnd.Next(5, 100);
            rentableItemsSettings.UpdatedByUserId = rnd.Next(1, 100);

            DbContext.RentableItemsSettings.Add(rentableItemsSettings);
            await DbContext.SaveChangesAsync();

            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();
            rentableItemsSettingsVer.CreatedByUserId = rentableItemsSettings.CreatedByUserId;
            rentableItemsSettingsVer.eFormId = rentableItemsSettings.eFormId.Value;
            rentableItemsSettingsVer.UpdatedByUserId = rentableItemsSettings.UpdatedByUserId;
            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            DbContext.RentableItemsSettingsVersions.Add(rentableItemsSettingsVer);
            await DbContext.SaveChangesAsync();
            // Act
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            rentableItemsSettingsModel.CreatedByUserID = rentableItemsSettings.CreatedByUserId;
            rentableItemsSettingsModel.eFormId = 555;
            rentableItemsSettingsModel.UpdatedByUserID = rentableItemsSettings.UpdatedByUserId;
            rentableItemsSettingsModel.Id = rentableItemsSettings.Id;
            await rentableItemsSettingsModel.Update(DbContext);
            
            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(2, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.CreatedByUserId);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.UpdatedByUserId);
            Assert.AreEqual(rentableItemsSettingsModel.eFormId, dbRentableItemsSettings.eFormId);


        }
        [Test]
        public async Task RentableItemSettingsModel_Delete_DoesDelete()
        {
            // Arrange
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();
            Random rnd = new Random();
            rentableItemsSettings.CreatedByUserId = rnd.Next(1, 82);
            rentableItemsSettings.eFormId = rnd.Next(5, 100);
            rentableItemsSettings.UpdatedByUserId = rnd.Next(1, 100);

            DbContext.RentableItemsSettings.Add(rentableItemsSettings);
            await DbContext.SaveChangesAsync();

            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();
            rentableItemsSettingsVer.CreatedByUserId = rentableItemsSettings.CreatedByUserId;
            rentableItemsSettingsVer.eFormId = rentableItemsSettings.eFormId.Value;
            rentableItemsSettingsVer.UpdatedByUserId = rentableItemsSettings.UpdatedByUserId;
            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            DbContext.RentableItemsSettingsVersions.Add(rentableItemsSettingsVer);
            await DbContext.SaveChangesAsync();
            // Act
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            rentableItemsSettingsModel.CreatedByUserID = rentableItemsSettings.CreatedByUserId;
            rentableItemsSettingsModel.eFormId = rentableItemsSettings.eFormId;
            rentableItemsSettingsModel.UpdatedByUserID = rentableItemsSettings.UpdatedByUserId;
            rentableItemsSettingsModel.Id = rentableItemsSettings.Id;

            await rentableItemsSettingsModel.Delete(DbContext);

            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(2, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.CreatedByUserId);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.UpdatedByUserId);
            Assert.AreEqual(rentableItemsSettingsModel.eFormId, dbRentableItemsSettings.eFormId);
            Assert.AreEqual(Constants.WorkflowStates.Removed, dbRentableItemsSettings.WorkflowState);


        }
    }
}
