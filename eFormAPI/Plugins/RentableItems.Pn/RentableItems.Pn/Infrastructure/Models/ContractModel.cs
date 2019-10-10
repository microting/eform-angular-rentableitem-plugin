﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microting.eForm.Infrastructure.Constants;
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
        public int? ContractNr { get; set; }
        public List<int> RentableItemIds { get; set; }
        
        public async Task Create(RentableItemsPnDbContext _dbContext)
        {
            Contract dbContract = _dbContext.Contract.FirstOrDefault(x => x.ContractNr == ContractNr);

            if (dbContract == null)
            {
                Contract contract = new Contract();
                Version = 1;
                contract.WorkflowState = Constants.WorkflowStates.Created;
                contract.Version = (int)Version;
                contract.CreatedAt = DateTime.Now;
                contract.UpdatedAt = DateTime.Now;
                contract.CreatedByUserId = CreatedByUserID;
                contract.UpdatedByUserId = UpdatedByUserID;
                contract.ContractStart = ContractStart;
                contract.ContractEnd = ContractEnd;
                contract.CustomerId = CustomerId;
                contract.ContractNr = ContractNr;
                
                _dbContext.Contract.Add(contract);
                await _dbContext.SaveChangesAsync();

                _dbContext.ContractVersions.Add(MapContract(_dbContext, contract));
               await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(RentableItemsPnDbContext _dbContext)
        {
            Contract contract = _dbContext.Contract.FirstOrDefault(x => x.Id == Id);

            if(contract == null)
            {
                throw new NullReferenceException($"Could not find Contract with id {Id}");
            }

            contract.CustomerId = CustomerId;
            contract.WorkflowState = contract.WorkflowState;
            contract.ContractNr = ContractNr;
            contract.ContractStart = ContractStart;
            contract.ContractEnd = ContractEnd;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                contract.UpdatedAt = DateTime.Now;
                contract.UpdatedByUserId = UpdatedByUserID;
                contract.Version += 1;

                _dbContext.ContractVersions.Add(MapContract(_dbContext, contract));
               await _dbContext.SaveChangesAsync();

            }

        }

        public async Task Delete(RentableItemsPnDbContext _dbContext)
        {
            Contract contract = _dbContext.Contract.FirstOrDefault(x => x.Id == Id);

            if (contract == null)
            {
                throw new NullReferenceException($"Could not find Contract with id {Id}");
            }

            contract.WorkflowState = Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                contract.UpdatedAt = DateTime.Now;
                contract.UpdatedByUserId = UpdatedByUserID;
                contract.Version += 1;

                _dbContext.ContractVersions.Add(MapContract(_dbContext, contract));
               await _dbContext.SaveChangesAsync();

            }
        }

        public ContractVersions MapContract(RentableItemsPnDbContext _dbContext, Contract contract)
        {
            ContractVersions contractVer = new ContractVersions();

            contractVer.ContractEnd = contract.ContractEnd;
            contractVer.ContractNr = contract.ContractNr;
            contractVer.ContractStart = contract.ContractStart;
            contractVer.CreatedAt = contract.CreatedAt;
            contractVer.CreatedByUserId = contract.CreatedByUserId;
            contractVer.Status = contract.Status;
            contractVer.UpdatedAt = contract.UpdatedAt;
            contractVer.UpdatedByUserId = contract.UpdatedByUserId;
            contractVer.Version = contract.Version;
            contractVer.WorkflowState = contract.WorkflowState;

            contractVer.ContractId = contract.Id;

            return contractVer;

        }
    }
}
