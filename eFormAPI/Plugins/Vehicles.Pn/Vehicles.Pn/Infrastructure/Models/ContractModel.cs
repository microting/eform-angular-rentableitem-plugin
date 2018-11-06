using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class ContractModel : IModel
    {
        public int Id { get; set; }
        public string WorkflowState { get; set; }
        public int? Version { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
        public int CustomerId { get; set; }
        public int ContractNr { get; set; }

        public void Save(RentableItemsPnDbAnySql _dbContext)
        {
            Contract contract = new Contract();

            contract.WorkflowState = eFormShared.Constants.WorkflowStates.Created;
            contract.Version = Version;
            contract.CreatedAt = DateTime.Now;
            contract.UpdatedAt = DateTime.Now;
            contract.Created_By_User_Id = CreatedByUserID;
            contract.Updated_By_User_Id = UpdatedByUserID;
            contract.ContractStart = ContractStart;
            contract.ContractEnd = ContractEnd;
            contract.CustomerId = CustomerId;
            contract.ContractNr = ContractNr;

            _dbContext.Contract.Add(contract);
            _dbContext.SaveChanges();

            _dbContext.ContractVersions.Add(MapContract(_dbContext, contract));
            _dbContext.SaveChanges();

        }

        public void Update(RentableItemsPnDbAnySql _dbContext)
        {
            Contract contract = _dbContext.Contract.FirstOrDefault(x => x.Id == Id);

            if(contract == null)
            {
                throw new NullReferenceException($"Could not find Contract with id {Id}");
            }

            contract.CustomerId = CustomerId;
            contract.WorkflowState = contract.WorkflowState;
            contract.ContractNr = ContractNr;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                contract.UpdatedAt = DateTime.Now;
                contract.Updated_By_User_Id = UpdatedByUserID;
                contract.Version += 1;

                _dbContext.ContractVersions.Add(MapContract(_dbContext, contract));
                _dbContext.SaveChanges();

            }

        }

        public void Delete(RentableItemsPnDbAnySql _dbContext)
        {
            Contract contract = _dbContext.Contract.FirstOrDefault(x => x.Id == Id);

            if (contract == null)
            {
                throw new NullReferenceException($"Could not find Contract with id {Id}");
            }

            contract.WorkflowState = eFormShared.Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                contract.UpdatedAt = DateTime.Now;
                contract.Updated_By_User_Id = UpdatedByUserID;
                contract.Version += 1;

                _dbContext.ContractVersions.Add(MapContract(_dbContext, contract));
                _dbContext.SaveChanges();

            }
        }

        public ContractVersions MapContract(RentableItemsPnDbAnySql _dbContext, Contract contract)
        {
            ContractVersions contractVer = new ContractVersions();

            contractVer.ContractEnd = contract.ContractEnd;
            contractVer.ContractNr = contract.ContractNr;
            contractVer.ContractStart = contract.ContractStart;
            contractVer.CreatedAt = contract.CreatedAt;
            contractVer.Created_By_User_Id = contract.Created_By_User_Id;
            contractVer.Status = contract.Status;
            contractVer.UpdatedAt = contract.UpdatedAt;
            contractVer.Updated_By_User_Id = contract.Updated_By_User_Id;
            contractVer.Version = contract.Version;
            contractVer.WorkflowState = contract.WorkflowState;

            contractVer.ContractId = contract.Id;

            return contractVer;

        }
    }
}
