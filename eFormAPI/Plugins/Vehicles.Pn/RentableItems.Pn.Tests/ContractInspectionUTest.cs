﻿using Microsoft.EntityFrameworkCore;
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
    public class ContractInspectionUTest : DbTestFixture
    {
        [Test]
        public void ContractInspectionModel_Save_DoesSave()
        {
            // Arrange
            #region creating Contract
            Contract contractModel = new Contract();
            Random rnd = new Random();
            DateTime contractEnd = DateTime.Now;
            DateTime contractStart = DateTime.Now;
            contractModel.ContractEnd = contractEnd;
            contractModel.ContractNr = rnd.Next(1, 123);
            contractModel.ContractStart = contractStart;
            contractModel.CustomerId = rnd.Next(1, 99);
            contractModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;
            DbContext.Contract.Add(contractModel);
            DbContext.SaveChanges();
            #endregion
            

            ContractInspectionModel contractInspectionModel = new ContractInspectionModel();
      
            DateTime doneAt = DateTime.UtcNow;
            contractInspectionModel.ContractId = contractModel.Id;
            contractInspectionModel.SdkCaseId = rnd.Next(1, 255);
            contractInspectionModel.CreatedByUserID = rnd.Next(1, 222);
            contractInspectionModel.DoneAt = doneAt;
            contractInspectionModel.UpdatedByUserID = rnd.Next(1, 333);
            contractInspectionModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            // Act
            contractInspectionModel.Save(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(1, versionList.Count());

            Assert.AreEqual(contractInspectionModel.ContractId, dbContractInspection.ContractId);
            Assert.AreEqual(contractInspectionModel.SdkCaseId, dbContractInspection.SDK_Case_Id);
            Assert.AreEqual(contractInspectionModel.CreatedByUserID, dbContractInspection.Created_By_User_Id);
            Assert.AreEqual(contractInspectionModel.DoneAt, dbContractInspection.DoneAt);
            Assert.AreEqual(contractInspectionModel.UpdatedByUserID, dbContractInspection.Updated_By_User_Id);
            Assert.AreEqual(contractInspectionModel.WorkflowState, dbContractInspection.WorkflowState);
        }
        [Test]
        public void ContractInspectionModel_Update_DoesUpdate()
        {
            // Arrange
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
            DbContext.SaveChanges();
            #endregion

            ContractInspection contractInspection = new ContractInspection();
            contractInspection.ContractId = contractModel.Id;
            contractInspection.SDK_Case_Id = rnd.Next(1, 666);

            DbContext.ContractInspection.Add(contractInspection);
            DbContext.SaveChanges();

            ContractInspectionVersion contractInspectionVersion = new ContractInspectionVersion();
            contractInspectionVersion.ContractInspectionId = contractInspection.Id;
            contractInspectionVersion.ContractId = contractInspection.ContractId;
            contractInspectionVersion.SDK_Case_Id = contractInspection.SDK_Case_Id;

            DbContext.ContractInspectionVersion.Add(contractInspectionVersion);
            DbContext.SaveChanges();
            // Act
            ContractInspectionModel contractInspectionModel = new ContractInspectionModel();
            contractInspectionModel.ContractId = contractModel.Id;
            contractInspectionModel.DoneAt = contractInspection.DoneAt;
            contractInspectionModel.Id = contractInspection.Id;
            contractInspectionModel.SdkCaseId = 55;
            contractInspectionModel.WorkflowState = contractInspection.WorkflowState;

            contractInspectionModel.Update(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(contractInspectionModel.ContractId, dbContractInspection.ContractId);
            Assert.AreEqual(contractInspectionModel.SdkCaseId, dbContractInspection.SDK_Case_Id);
            Assert.AreEqual(contractInspectionModel.CreatedByUserID, dbContractInspection.Created_By_User_Id);
            Assert.AreEqual(contractInspectionModel.DoneAt, dbContractInspection.DoneAt);
            Assert.AreEqual(contractInspectionModel.UpdatedByUserID, dbContractInspection.Updated_By_User_Id);
            Assert.AreEqual(contractInspectionModel.WorkflowState, dbContractInspection.WorkflowState);

        }
        [Test]
        public void ContractInspectionModel_Delete_DoesDelete()
        {
            // Arrange
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
            DbContext.SaveChanges();
            #endregion

            ContractInspection contractInspection = new ContractInspection();
            contractInspection.ContractId = contractModel.Id;
            contractInspection.SDK_Case_Id = rnd.Next(1, 666);

            DbContext.ContractInspection.Add(contractInspection);
            DbContext.SaveChanges();

            ContractInspectionVersion contractInspectionVersion = new ContractInspectionVersion();
            contractInspectionVersion.ContractInspectionId = contractInspection.Id;
            contractInspectionVersion.ContractId = contractInspection.ContractId;
            contractInspectionVersion.SDK_Case_Id = contractInspection.SDK_Case_Id;

            DbContext.ContractInspectionVersion.Add(contractInspectionVersion);
            DbContext.SaveChanges();
            // Act
            ContractInspectionModel contractInspectionModel = new ContractInspectionModel();
            contractInspectionModel.ContractId = contractModel.Id;
            contractInspectionModel.DoneAt = contractInspection.DoneAt;
            contractInspectionModel.Id = contractInspection.Id;
            //contractInspectionModel.SdkCaseId = contractInspection.SDK_Case_Id;
            contractInspectionModel.WorkflowState = contractInspection.WorkflowState;

            contractInspectionModel.Delete(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(2, versionList.Count());

            Assert.AreEqual(contractInspectionModel.ContractId, dbContractInspection.ContractId);
            //Assert.AreEqual(contractInspectionModel.SdkCaseId, dbContractInspection.SDK_Case_Id);
            Assert.AreEqual(contractInspectionModel.CreatedByUserID, dbContractInspection.Created_By_User_Id);
            Assert.AreEqual(contractInspectionModel.DoneAt, dbContractInspection.DoneAt);
            Assert.AreEqual(contractInspectionModel.UpdatedByUserID, dbContractInspection.Updated_By_User_Id);
            Assert.AreEqual(eFormShared.Constants.WorkflowStates.Removed, dbContractInspection.WorkflowState);
        }
    }
}
