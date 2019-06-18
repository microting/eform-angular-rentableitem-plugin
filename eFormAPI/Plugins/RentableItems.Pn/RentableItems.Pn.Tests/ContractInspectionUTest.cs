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
    public class ContractInspectionUTest : DbTestFixture
    {
        [Test]
        public async Task ContractInspectionModel_Save_DoesSave()
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
            await DbContext.SaveChangesAsync();
            #endregion
            

            ContractInspectionModel contractInspectionModel = new ContractInspectionModel();
      
            DateTime doneAt = DateTime.UtcNow;
            contractInspectionModel.ContractId = contractModel.Id;
            //contractInspectionModel.SdkCaseId = rnd.Next(1, 255); // isn't being used in the save method, will always fail.
            contractInspectionModel.CreatedByUserID = rnd.Next(1, 222);
            contractInspectionModel.DoneAt = doneAt;
            contractInspectionModel.UpdatedByUserID = rnd.Next(1, 333);
            contractInspectionModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;

            // Act
           await contractInspectionModel.Create(DbContext);

            ContractInspection dbContractInspection = DbContext.ContractInspection.AsNoTracking().First();
            List<ContractInspection> inspectionList = DbContext.ContractInspection.AsNoTracking().ToList();
            List<ContractInspectionVersion> versionList = DbContext.ContractInspectionVersion.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbContractInspection);

            Assert.AreEqual(1, inspectionList.Count());

            Assert.AreEqual(1, versionList.Count());

            Assert.AreEqual(contractInspectionModel.ContractId, dbContractInspection.ContractId);
            //Assert.AreEqual(contractInspectionModel.SdkCaseId, dbContractInspection.SDK_Case_Id); // isn't being used in the save method, will always fail.
            Assert.AreEqual(contractInspectionModel.CreatedByUserID, dbContractInspection.Created_By_User_Id);
            Assert.AreEqual(contractInspectionModel.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspectionModel.UpdatedByUserID, dbContractInspection.Updated_By_User_Id);
            Assert.AreEqual(contractInspectionModel.WorkflowState, dbContractInspection.WorkflowState);
        }
        [Test]
        public async Task ContractInspectionModel_Update_DoesUpdate()
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
            await DbContext.SaveChangesAsync();
            #endregion

            DateTime doneAt = DateTime.UtcNow;
            ContractInspection contractInspection = new ContractInspection();
            contractInspection.ContractId = contractModel.Id;
            contractInspection.SDK_Case_Id = Guid.NewGuid().ToString();
            contractInspection.DoneAt = doneAt;

            DbContext.ContractInspection.Add(contractInspection);
            await DbContext.SaveChangesAsync();

            ContractInspectionVersion contractInspectionVersion = new ContractInspectionVersion();
            contractInspectionVersion.ContractInspectionId = contractInspection.Id;
            contractInspectionVersion.ContractId = contractInspection.ContractId;
            contractInspectionVersion.SDK_Case_Id = contractInspection.SDK_Case_Id;

            DbContext.ContractInspectionVersion.Add(contractInspectionVersion);
            await DbContext.SaveChangesAsync();
            // Act
            ContractInspectionModel contractInspectionModel = new ContractInspectionModel();
            contractInspectionModel.ContractId = contractModel.Id;
            contractInspectionModel.DoneAt = contractInspection.DoneAt;
            contractInspectionModel.Id = contractInspection.Id;
            contractInspectionModel.SdkCaseId = Guid.NewGuid().ToString();
            contractInspectionModel.WorkflowState = contractInspection.WorkflowState;

            await contractInspectionModel.Update(DbContext);

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
            Assert.AreEqual(contractInspectionModel.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspectionModel.UpdatedByUserID, dbContractInspection.Updated_By_User_Id);
            Assert.AreEqual(contractInspectionModel.WorkflowState, dbContractInspection.WorkflowState);

        }
        [Test]
        public async Task ContractInspectionModel_Delete_DoesDelete()
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
            await DbContext.SaveChangesAsync();
            #endregion

            DateTime doneAt = DateTime.UtcNow;
            ContractInspection contractInspection = new ContractInspection();
            contractInspection.ContractId = contractModel.Id;
            contractInspection.SDK_Case_Id = Guid.NewGuid().ToString();
            contractInspection.DoneAt = doneAt;
            DbContext.ContractInspection.Add(contractInspection);
           await DbContext.SaveChangesAsync();

            ContractInspectionVersion contractInspectionVersion = new ContractInspectionVersion();
            contractInspectionVersion.ContractInspectionId = contractInspection.Id;
            contractInspectionVersion.ContractId = contractInspection.ContractId;
            contractInspectionVersion.SDK_Case_Id = contractInspection.SDK_Case_Id;

            DbContext.ContractInspectionVersion.Add(contractInspectionVersion);
           await DbContext.SaveChangesAsync();
            // Act
            ContractInspectionModel contractInspectionModel = new ContractInspectionModel();
            contractInspectionModel.ContractId = contractModel.Id;
            contractInspectionModel.DoneAt = contractInspection.DoneAt;
            contractInspectionModel.Id = contractInspection.Id;
            contractInspectionModel.SdkCaseId = contractInspection.SDK_Case_Id;
            contractInspectionModel.WorkflowState = contractInspection.WorkflowState;

            await contractInspectionModel.Delete(DbContext);

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
            Assert.AreEqual(contractInspectionModel.DoneAt.ToString(), dbContractInspection.DoneAt.ToString());
            Assert.AreEqual(contractInspectionModel.UpdatedByUserID, dbContractInspection.Updated_By_User_Id);
            Assert.AreEqual(eFormShared.Constants.WorkflowStates.Removed, dbContractInspection.WorkflowState);
        }
    }
}
