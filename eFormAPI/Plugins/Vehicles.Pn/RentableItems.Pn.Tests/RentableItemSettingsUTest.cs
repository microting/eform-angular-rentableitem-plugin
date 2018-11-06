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
    public class RentableItemSettingsUTest : DbTestFixture
    {
        [Test]
        public void RentableItemSettingsModel_Save_DoesSave()
        {
            // Arrange
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            Random rnd = new Random();
            rentableItemsSettingsModel.CreatedByUserID = rnd.Next(1, 541);
            rentableItemsSettingsModel.UpdatedByUserID = rnd.Next(1, 541);
            rentableItemsSettingsModel.EformId = rnd.Next(123, 582);

            // Act
            rentableItemsSettingsModel.Save(DbContext);

            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(1, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.Created_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.Updated_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.EformId, dbRentableItemsSettings.Eform_Id);

        }
        [Test]
        public void RentableItemSettingsModel_Update_DoesUpdate()
        {
            // Arrange
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();
            Random rnd = new Random();
            rentableItemsSettings.Created_By_User_Id = rnd.Next(1, 82);
            rentableItemsSettings.Eform_Id = rnd.Next(5, 100);
            rentableItemsSettings.Updated_By_User_Id = rnd.Next(1, 100);

            DbContext.RentableItemsSettings.Add(rentableItemsSettings);
            DbContext.SaveChanges();

            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();
            rentableItemsSettingsVer.Created_By_User_Id = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsVer.Eform_Id = rentableItemsSettings.Eform_Id;
            rentableItemsSettingsVer.Updated_By_User_Id = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            DbContext.RentableItemsSettingsVersions.Add(rentableItemsSettingsVer);
            DbContext.SaveChanges();
            // Act
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            rentableItemsSettingsModel.CreatedByUserID = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsModel.EformId = 555;
            rentableItemsSettingsModel.UpdatedByUserID = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsModel.Id = rentableItemsSettings.Id;
            rentableItemsSettingsModel.Update(DbContext);
            
            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(2, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.Created_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.Updated_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.EformId, dbRentableItemsSettings.Eform_Id);


        }
        [Test]
        public void RentableItemSettingsModel_Delete_DoesDelete()
        {
            // Arrange
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();
            Random rnd = new Random();
            rentableItemsSettings.Created_By_User_Id = rnd.Next(1, 82);
            rentableItemsSettings.Eform_Id = rnd.Next(5, 100);
            rentableItemsSettings.Updated_By_User_Id = rnd.Next(1, 100);

            DbContext.RentableItemsSettings.Add(rentableItemsSettings);
            DbContext.SaveChanges();

            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();
            rentableItemsSettingsVer.Created_By_User_Id = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsVer.Eform_Id = rentableItemsSettings.Eform_Id;
            rentableItemsSettingsVer.Updated_By_User_Id = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            DbContext.RentableItemsSettingsVersions.Add(rentableItemsSettingsVer);
            DbContext.SaveChanges();
            // Act
            RentableItemsSettingsModel rentableItemsSettingsModel = new RentableItemsSettingsModel();
            rentableItemsSettingsModel.CreatedByUserID = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsModel.EformId = rentableItemsSettings.Eform_Id;
            rentableItemsSettingsModel.UpdatedByUserID = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsModel.Id = rentableItemsSettings.Id;

            rentableItemsSettingsModel.Delete(DbContext);

            RentableItemsSettings dbRentableItemsSettings = DbContext.RentableItemsSettings.AsNoTracking().First();
            List<RentableItemsSettings> settingsList = DbContext.RentableItemsSettings.AsNoTracking().ToList();
            List<RentableItemsSettingsVersions> versionsList = DbContext.RentableItemsSettingsVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbRentableItemsSettings);

            Assert.AreEqual(1, settingsList.Count());

            Assert.AreEqual(2, versionsList.Count());

            Assert.AreEqual(rentableItemsSettingsModel.CreatedByUserID, dbRentableItemsSettings.Created_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.UpdatedByUserID, dbRentableItemsSettings.Updated_By_User_Id);
            Assert.AreEqual(rentableItemsSettingsModel.EformId, dbRentableItemsSettings.Eform_Id);
            Assert.AreEqual(eFormShared.Constants.WorkflowStates.Removed, dbRentableItemsSettings.Workflow_state);


        }
    }
}
