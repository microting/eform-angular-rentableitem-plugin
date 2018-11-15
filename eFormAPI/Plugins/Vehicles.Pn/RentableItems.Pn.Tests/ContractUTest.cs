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
    public class ContractUTest : DbTestFixture
    {
        [Test]
        public void ContractModel_Save_DoesSave()
        {
            // Arrange
            ContractModel contractModel = new ContractModel();
            Random rnd = new Random();
            //DateTime contractEnd = DateTime.UtcNow;
            //DateTime contractStart = DateTime.UtcNow;
            contractModel.ContractEnd = DateTime.UtcNow;
            contractModel.ContractNr =  rnd.Next(1, 123);
            contractModel.ContractStart = DateTime.UtcNow;
            contractModel.CustomerId = rnd.Next(1, 99);
            contractModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            // Act
            contractModel.Save(DbContext);

            Contract dbContract = DbContext.Contract.AsNoTracking().First();
            List<Contract> contractList = DbContext.Contract.AsNoTracking().ToList();
            List<ContractVersions> versionList = DbContext.ContractVersions.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(dbContract);

            Assert.AreEqual(1, contractList.Count());

            Assert.AreEqual(1, versionList.Count());

            //Assert.AreEqual(contractModel.ContractEnd, dbContract.ContractEnd);
            Assert.AreEqual(contractModel.ContractNr, dbContract.ContractNr);
            Assert.AreEqual(contractModel.ContractStart, dbContract.ContractStart);
            Assert.AreEqual(contractModel.CustomerId, dbContract.CustomerId);
            Assert.AreEqual(contractModel.WorkflowState, dbContract.WorkflowState);
        }

        [Test]
        public void ContractModel_Update_DoesUpdate()
        {
            // Arrange
            Contract contract = new Contract();
            Random rnd = new Random();
            //DateTime contractEnd = DateTime.UtcNow;
            //DateTime contractStart = DateTime.UtcNow;
            contract.ContractEnd = DateTime.UtcNow;
            contract.ContractNr = rnd.Next(1, 123);
            contract.ContractStart = DateTime.UtcNow;
            contract.CustomerId = rnd.Next(1, 99);
            contract.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            DbContext.Contract.Add(contract);
            DbContext.SaveChanges();

            ContractVersions contractVer = new ContractVersions();
            contractVer.ContractEnd = contract.ContractEnd;
            contractVer.ContractNr = contract.ContractNr;
            contractVer.ContractStart = contract.ContractStart;
            contractVer.CustomerId = contract.CustomerId;
            contractVer.WorkflowState = contract.WorkflowState;
            contractVer.ContractId = contract.Id;

            DbContext.ContractVersions.Add(contractVer);
            DbContext.SaveChanges();

            // Act
            ContractModel contractModel = new ContractModel();
            contractModel.ContractEnd = contract.ContractEnd;
            contractModel.ContractNr = 51200;
            contractModel.ContractStart = contract.ContractStart;
            contractModel.CreatedAt = contract.CreatedAt;
            contractModel.CreatedByUserID = contract.Created_By_User_Id;
            contractModel.CustomerId = contract.CustomerId;
            contractModel.Id = contract.Id;
            contractModel.UpdatedAt = contract.UpdatedAt;
            contractModel.UpdatedByUserID = contract.Updated_By_User_Id;
            contractModel.Version = contract.Version;
            contractModel.WorkflowState = contract.WorkflowState;

            contractModel.Update(DbContext);

            Contract dbContract = DbContext.Contract.AsNoTracking().First();
            List<Contract> contractList = DbContext.Contract.AsNoTracking().ToList();
            List<ContractVersions> versionList = DbContext.ContractVersions.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(contract);

            Assert.AreEqual(1, contractList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(contract.ContractEnd, dbContract.ContractEnd);
            Assert.AreEqual(contract.ContractNr, dbContract.ContractNr);
            Assert.AreEqual(contract.ContractStart, dbContract.ContractStart);
            Assert.AreEqual(contract.CustomerId, dbContract.CustomerId);
            Assert.AreEqual(contract.WorkflowState, dbContract.WorkflowState);
        }

        [Test]
        public void ContractModel_Delete_DoesDelete()
        {
            // Arrange
            Contract contract = new Contract();
            Random rnd = new Random();
            //DateTime contractEnd = DateTime.UtcNow;
            //DateTime contractStart = DateTime.UtcNow;
            contract.ContractEnd = DateTime.UtcNow;
            contract.ContractNr = rnd.Next(1, 123);
            contract.ContractStart = DateTime.UtcNow;
            contract.CustomerId = rnd.Next(1, 99);
            contract.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            DbContext.Contract.Add(contract);
            DbContext.SaveChanges();

            ContractVersions contractVer = new ContractVersions();
            contractVer.ContractEnd = contract.ContractEnd;
            contractVer.ContractNr = contract.ContractNr;
            contractVer.ContractStart = contract.ContractStart;
            contractVer.CustomerId = contract.CustomerId;
            contractVer.WorkflowState = contract.WorkflowState;
            contractVer.ContractId = contract.Id;

            DbContext.ContractVersions.Add(contractVer);
            DbContext.SaveChanges();

            // Act
            ContractModel contractModel = new ContractModel();
            contractModel.ContractEnd = contract.ContractEnd;
            contractModel.ContractNr = contract.ContractNr;
            contractModel.ContractStart = contract.ContractStart;
            contractModel.CreatedAt = contract.CreatedAt;
            contractModel.CreatedByUserID = contract.Created_By_User_Id;
            contractModel.CustomerId = contract.CustomerId;
            contractModel.Id = contract.Id;
            contractModel.UpdatedAt = contract.UpdatedAt;
            contractModel.UpdatedByUserID = contract.Updated_By_User_Id;
            contractModel.Version = contract.Version;
            contractModel.WorkflowState = contract.WorkflowState;

            contractModel.Delete(DbContext);

            Contract dbContract = DbContext.Contract.AsNoTracking().First();
            List<Contract> contractList = DbContext.Contract.AsNoTracking().ToList();
            List<ContractVersions> versionList = DbContext.ContractVersions.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(contract);

            Assert.AreEqual(1, contractList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(contract.ContractEnd, dbContract.ContractEnd);
            Assert.AreEqual(contract.ContractNr, dbContract.ContractNr);
            Assert.AreEqual(contract.ContractStart, dbContract.ContractStart);
            Assert.AreEqual(contract.CustomerId, dbContract.CustomerId);
            Assert.AreEqual(eFormShared.Constants.WorkflowStates.Removed, dbContract.WorkflowState);

        }
    }
}
