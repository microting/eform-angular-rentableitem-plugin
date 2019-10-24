using System;
using System.Linq;
using System.Threading.Tasks;
using Microting.eForm.Infrastructure.Constants;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemContractModel : IModel
    {
        public int Id { get; set; }
        public string WorkflowState { get; set; }
        public int? Version { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public int RentableItemId { get; set; }
        public int ContractId { get; set; }
        public async Task Create(RentableItemsPnDbContext _dbContext)
        {
            RentableItemContract rentableItemContract = new RentableItemContract();

            rentableItemContract.ContractId = ContractId;
            rentableItemContract.RentableItemId = RentableItemId;
            rentableItemContract.WorkflowState = Constants.WorkflowStates.Created;
            rentableItemContract.CreatedAt = DateTime.Now;
            rentableItemContract.UpdatedAt = DateTime.Now;
            rentableItemContract.CreatedByUserId = CreatedByUserID;
            rentableItemContract.UpdatedByUserId = UpdatedByUserID;
            _dbContext.RentableItemContract.Add(rentableItemContract);
            await _dbContext.SaveChangesAsync();

            _dbContext.RentableItemsContractVersions.Add(MapRentableItemContractVersions(_dbContext, rentableItemContract));
            await _dbContext.SaveChangesAsync();

        }
        public async Task Update(RentableItemsPnDbContext _dbContext)
        {
            RentableItemContract rentableItemContract = _dbContext.RentableItemContract.FirstOrDefault(x => x.Id == Id);

            if (rentableItemContract == null)
            {
                throw new NullReferenceException($"Could not find RentableItem Contract with id {Id}");
            }

            rentableItemContract.ContractId = ContractId;
            rentableItemContract.RentableItemId = RentableItemId;
            rentableItemContract.WorkflowState = rentableItemContract.WorkflowState;
            
            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemContract.UpdatedAt = DateTime.Now;
                rentableItemContract.UpdatedByUserId = UpdatedByUserID;
                rentableItemContract.Version += 1;

                _dbContext.RentableItemsContractVersions.Add(MapRentableItemContractVersions(_dbContext, rentableItemContract));
               await _dbContext.SaveChangesAsync();

            }
        }
        public async Task Delete(RentableItemsPnDbContext _dbContext)
        {
            RentableItemContract rentableItemContract = _dbContext.RentableItemContract.FirstOrDefault(x => x.Id == Id);

            if (rentableItemContract == null)
            {
                throw new NullReferenceException($"Could not find RentableItem Contract with id {Id}");
            }


            rentableItemContract.WorkflowState = Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemContract.UpdatedAt = DateTime.Now;
                rentableItemContract.UpdatedByUserId = UpdatedByUserID;
                rentableItemContract.Version += 1;

                _dbContext.RentableItemsContractVersions.Add(MapRentableItemContractVersions(_dbContext, rentableItemContract));
               await _dbContext.SaveChangesAsync();

            }
        }

        public RentableItemsContractVersions MapRentableItemContractVersions(RentableItemsPnDbContext _dbContext, RentableItemContract rentableItemContract)
        {
            RentableItemsContractVersions rentableItemscontractVer = new RentableItemsContractVersions();

            rentableItemscontractVer.ContractId = rentableItemContract.ContractId;
            rentableItemscontractVer.CreatedAt = rentableItemContract.CreatedAt;
            rentableItemscontractVer.CreatedByUserId = rentableItemContract.CreatedByUserId;
            rentableItemscontractVer.RentableItemId = rentableItemContract.RentableItemId;
            rentableItemscontractVer.UpdatedAt = rentableItemContract.UpdatedAt;
            rentableItemscontractVer.UpdatedByUserId = rentableItemContract.UpdatedByUserId;
            rentableItemscontractVer.Version = rentableItemContract.Version;
            rentableItemscontractVer.WorkflowState = rentableItemContract.WorkflowState;

            rentableItemscontractVer.RentableItemContractId = rentableItemContract.Id;

            return rentableItemscontractVer;

        }
    }
}
