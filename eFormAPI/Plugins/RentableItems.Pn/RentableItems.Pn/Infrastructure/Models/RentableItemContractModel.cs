using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            rentableItemContract.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItemContract.Created_at = DateTime.Now;
            rentableItemContract.Updated_at = DateTime.Now;
            rentableItemContract.Created_By_User_Id = CreatedByUserID;
            rentableItemContract.Updated_By_User_Id = UpdatedByUserID;

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
            rentableItemContract.Workflow_state = rentableItemContract.Workflow_state;
            
            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemContract.Updated_at = DateTime.Now;
                rentableItemContract.Updated_By_User_Id = UpdatedByUserID;
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


            rentableItemContract.Workflow_state = eFormShared.Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemContract.Updated_at = DateTime.Now;
                rentableItemContract.Updated_By_User_Id = UpdatedByUserID;
                rentableItemContract.Version += 1;

                _dbContext.RentableItemsContractVersions.Add(MapRentableItemContractVersions(_dbContext, rentableItemContract));
               await _dbContext.SaveChangesAsync();

            }
        }

        public RentableItemsContractVersions MapRentableItemContractVersions(RentableItemsPnDbContext _dbContext, RentableItemContract rentableItemContract)
        {
            RentableItemsContractVersions rentableItemscontractVer = new RentableItemsContractVersions();

            rentableItemscontractVer.ContractId = rentableItemContract.ContractId;
            rentableItemscontractVer.Created_at = rentableItemContract.Created_at;
            rentableItemscontractVer.Created_By_User_Id = rentableItemContract.Created_By_User_Id;
            rentableItemscontractVer.RentableItemId = rentableItemContract.RentableItemId;
            rentableItemscontractVer.Updated_at = rentableItemContract.Updated_at;
            rentableItemscontractVer.Updated_By_User_Id = rentableItemContract.Updated_By_User_Id;
            rentableItemscontractVer.Version = rentableItemContract.Version;
            rentableItemscontractVer.Workflow_state = rentableItemContract.Workflow_state;

            rentableItemscontractVer.RentableItemContractId = rentableItemContract.Id;

            return rentableItemscontractVer;

        }
    }
}
