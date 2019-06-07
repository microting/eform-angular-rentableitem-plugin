using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentableItems.Pn.Tests
{
    [TestFixture]
    public class RentableItemContractUTest : DbTestFixture
    {
        [Test]
        public async Task RentableItemContractModel_Save_DoesSave()
        {
            // Arrange
            #region Create
            #region creating Item
            RentableItem rentableItemModel = new RentableItem();
            rentableItemModel.Brand = Guid.NewGuid().ToString();
            rentableItemModel.ModelName = Guid.NewGuid().ToString();
            rentableItemModel.PlateNumber = Guid.NewGuid().ToString();
            rentableItemModel.VinNumber = Guid.NewGuid().ToString();
            rentableItemModel.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItemModel.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItemModel.RegistrationDate = registrationDate;
            DbContext.RentableItem.Add(rentableItemModel);
            await DbContext.SaveChangesAsync();
            #endregion

            #region creating Contract
            Contract contractModel = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.UtcNow;
            DateTime contractStart = DateTime.UtcNow;
            contractModel.ContractEnd = contractEnd;
            contractModel.ContractNr = rnd.Next(1, 123);
            contractModel.ContractStart = contractStart;
            contractModel.CustomerId = rnd.Next(1, 99);
            contractModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;
            DbContext.Contract.Add(contractModel);
          await DbContext.SaveChangesAsync();
            #endregion
            #endregion

            RentableItemContractModel rentableItemContractModel = new RentableItemContractModel();

            rentableItemContractModel.ContractId = contractModel.Id;
            rentableItemContractModel.RentableItemId = rentableItemModel.Id;
            rentableItemContractModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;
            // Act
           await rentableItemContractModel.Save(DbContext);

            RentableItemContract rentableItemcontract = DbContext.RentableItemContract.AsNoTracking().First();
            List<RentableItemContract> rentableItemcontractList = DbContext.RentableItemContract.AsNoTracking().ToList();
            List<RentableItemsContractVersions> versionList = DbContext.RentableItemsContractVersions.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(rentableItemcontract);

            Assert.AreEqual(1, rentableItemcontractList.Count());

            Assert.AreEqual(1, versionList.Count());

            Assert.AreEqual(rentableItemContractModel.ContractId, rentableItemcontract.ContractId);
            Assert.AreEqual(rentableItemContractModel.RentableItemId, rentableItemcontract.RentableItemId);
            Assert.AreEqual(rentableItemContractModel.WorkflowState, rentableItemcontract.Workflow_state);

        }
        [Test]
        public async Task RentableItemContractModel_Update_DoesUpdate()
        {
            // Arrange
            #region Create
            #region create Item
            RentableItem rentableItemModel = new RentableItem();
            rentableItemModel.Brand = Guid.NewGuid().ToString();
            rentableItemModel.ModelName = Guid.NewGuid().ToString();
            rentableItemModel.PlateNumber = Guid.NewGuid().ToString();
            rentableItemModel.VinNumber = Guid.NewGuid().ToString();
            rentableItemModel.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItemModel.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItemModel.RegistrationDate = registrationDate;

            DbContext.RentableItem.Add(rentableItemModel);
            await DbContext.SaveChangesAsync();
            #endregion

            #region create Contract
            Contract contractModel = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.UtcNow;
            DateTime contractStart = DateTime.UtcNow;
            contractModel.ContractEnd = contractEnd;
            contractModel.ContractNr = rnd.Next(1, 123);
            contractModel.ContractStart = contractStart;
            contractModel.CustomerId = rnd.Next(1, 99);
            contractModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            DbContext.Contract.Add(contractModel);
           await DbContext.SaveChangesAsync();
            #endregion

            #region create rentableContract
            RentableItemContract rentableItemContract = new RentableItemContract();
            rentableItemContract.ContractId = contractModel.Id;
            rentableItemContract.RentableItemId = rentableItemModel.Id;

            DbContext.RentableItemContract.Add(rentableItemContract);
            await DbContext.SaveChangesAsync();
            #endregion

            #region create version
            RentableItemsContractVersions rentableItemsContractVersions = new RentableItemsContractVersions();
            rentableItemsContractVersions.RentableItemContractId = rentableItemContract.Id;
            rentableItemsContractVersions.ContractId = rentableItemContract.ContractId;
            rentableItemsContractVersions.RentableItemId = rentableItemContract.RentableItemId;

            DbContext.RentableItemsContractVersions.Add(rentableItemsContractVersions);
            await DbContext.SaveChangesAsync();

            #endregion
            #endregion

            // Act
            RentableItemContractModel rentableItemContractModel = new RentableItemContractModel();
            rentableItemContractModel.ContractId = 5;
            rentableItemContractModel.RentableItemId = rentableItemContract.RentableItemId;
            rentableItemContractModel.Id = rentableItemContract.Id;
            rentableItemContractModel.WorkflowState = rentableItemContract.Workflow_state;
            await rentableItemContractModel.Update(DbContext);

            RentableItemContract rentableItemcontract = DbContext.RentableItemContract.AsNoTracking().First();
            List<RentableItemContract> rentableItemcontractList = DbContext.RentableItemContract.AsNoTracking().ToList();
            List<RentableItemsContractVersions> versionList = DbContext.RentableItemsContractVersions.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(rentableItemcontract);

            Assert.AreEqual(1, rentableItemcontractList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(rentableItemContractModel.ContractId, rentableItemcontract.ContractId);
            Assert.AreEqual(rentableItemContractModel.RentableItemId, rentableItemcontract.RentableItemId);
            Assert.AreEqual(rentableItemContractModel.WorkflowState, rentableItemcontract.Workflow_state);

        }
        [Test]
        public async Task RentableItemContractModel_Delete_DoesDelete()
        {
            // Arrange
            #region Create
            #region create Item
            RentableItem rentableItemModel = new RentableItem();
            rentableItemModel.Brand = Guid.NewGuid().ToString();
            rentableItemModel.ModelName = Guid.NewGuid().ToString();
            rentableItemModel.PlateNumber = Guid.NewGuid().ToString();
            rentableItemModel.VinNumber = Guid.NewGuid().ToString();
            rentableItemModel.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItemModel.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.UtcNow;
            rentableItemModel.RegistrationDate = registrationDate;

            DbContext.RentableItem.Add(rentableItemModel);
            await DbContext.SaveChangesAsync();
            #endregion

            #region create Contract
            Contract contractModel = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.UtcNow;
            DateTime contractStart = DateTime.UtcNow;
            contractModel.ContractEnd = contractEnd;
            contractModel.ContractNr = rnd.Next(1, 123);
            contractModel.ContractStart = contractStart;
            contractModel.CustomerId = rnd.Next(1, 99);
            contractModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            DbContext.Contract.Add(contractModel);
            await DbContext.SaveChangesAsync();
            #endregion

            #region create rentableContract
            RentableItemContract rentableItemContract = new RentableItemContract();
            rentableItemContract.ContractId = contractModel.Id;
            rentableItemContract.RentableItemId = rentableItemModel.Id;

            DbContext.RentableItemContract.Add(rentableItemContract);
            await DbContext.SaveChangesAsync();
            #endregion

            #region create version
            RentableItemsContractVersions rentableItemsContractVersions = new RentableItemsContractVersions();
            rentableItemsContractVersions.RentableItemContractId = rentableItemContract.Id;
            rentableItemsContractVersions.ContractId = rentableItemContract.ContractId;
            rentableItemsContractVersions.RentableItemId = rentableItemContract.RentableItemId;

            DbContext.RentableItemsContractVersions.Add(rentableItemsContractVersions);
            await DbContext.SaveChangesAsync();

            #endregion
            #endregion

            // Act
            RentableItemContractModel rentableItemContractModel = new RentableItemContractModel();
            rentableItemContractModel.ContractId = rentableItemContract.ContractId;
            rentableItemContractModel.RentableItemId = rentableItemContract.RentableItemId;
            rentableItemContractModel.Id = rentableItemContract.Id;
            rentableItemContractModel.WorkflowState = rentableItemContract.Workflow_state;

            await rentableItemContractModel.Delete(DbContext);

            RentableItemContract rentableItemcontract = DbContext.RentableItemContract.AsNoTracking().First();
            List<RentableItemContract> rentableItemcontractList = DbContext.RentableItemContract.AsNoTracking().ToList();
            List<RentableItemsContractVersions> versionList = DbContext.RentableItemsContractVersions.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(rentableItemcontract);

            Assert.AreEqual(1, rentableItemcontractList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(rentableItemContractModel.ContractId, rentableItemcontract.ContractId);
            Assert.AreEqual(rentableItemContractModel.RentableItemId, rentableItemcontract.RentableItemId);
            Assert.AreEqual(eFormShared.Constants.WorkflowStates.Removed, rentableItemcontract.Workflow_state);

        }
    }
}
