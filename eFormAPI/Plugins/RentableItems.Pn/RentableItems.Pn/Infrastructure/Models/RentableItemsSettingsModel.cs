using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemsSettingsModel : IModel
    {
        public int Id { get; set; }
        public string WorkflowState { get; set; }
        public int Version { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public int? eFormId { get; set; }
        
        public async Task Save(RentableItemsPnDbContext _dbContext)
        {
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();

            rentableItemsSettings.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItemsSettings.Version = Version;
            rentableItemsSettings.Created_at = DateTime.Now;
            rentableItemsSettings.Updated_at = DateTime.Now;
            rentableItemsSettings.Created_By_User_Id = CreatedByUserID;
            rentableItemsSettings.Updated_By_User_Id = UpdatedByUserID;
            rentableItemsSettings.eForm_Id = eFormId;

            _dbContext.RentableItemsSettings.Add(rentableItemsSettings);
           await _dbContext.SaveChangesAsync();

            _dbContext.RentableItemsSettingsVersions.Add(MapRentableItemsSettings(_dbContext, rentableItemsSettings));
           await _dbContext.SaveChangesAsync();

            

        }

        public async Task Update(RentableItemsPnDbContext _dbContext)
        {
            RentableItemsSettings rentableItemsSettings = _dbContext.RentableItemsSettings.FirstOrDefault(x => x.Id == Id);

            if(rentableItemsSettings == null)
            {
                throw new NullReferenceException($"Could not find RentableItem Setting with id {Id}");
            }

            rentableItemsSettings.eForm_Id = eFormId;
            rentableItemsSettings.Workflow_state = rentableItemsSettings.Workflow_state;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemsSettings.Updated_at = DateTime.Now;
                rentableItemsSettings.Updated_By_User_Id = UpdatedByUserID;
                rentableItemsSettings.Version += 1;

                _dbContext.RentableItemsSettingsVersions.Add(MapRentableItemsSettings(_dbContext, rentableItemsSettings));
              await _dbContext.SaveChangesAsync();

            }
        }

        public async Task Delete(RentableItemsPnDbContext _dbContext)
        {
            RentableItemsSettings rentableItemsSettings = _dbContext.RentableItemsSettings.FirstOrDefault(x => x.Id == Id);

            if (rentableItemsSettings == null)
            {
                throw new NullReferenceException($"Could not find RentableItem Setting with id {Id}");
            }

            rentableItemsSettings.Workflow_state = eFormShared.Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemsSettings.Updated_at = DateTime.Now;
                rentableItemsSettings.Updated_By_User_Id = UpdatedByUserID;
                rentableItemsSettings.Version += 1;

                _dbContext.RentableItemsSettingsVersions.Add(MapRentableItemsSettings(_dbContext, rentableItemsSettings));
               await _dbContext.SaveChangesAsync();

            }
        }
        public RentableItemsSettingsVersions MapRentableItemsSettings(RentableItemsPnDbContext _dbContext, RentableItemsSettings rentableItemsSettings)
        {
            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();

            rentableItemsSettingsVer.Created_at = rentableItemsSettings.Created_at;
            rentableItemsSettingsVer.Created_By_User_Id = rentableItemsSettings.Created_By_User_Id;
            rentableItemsSettingsVer.Eform_Id = rentableItemsSettingsVer.Eform_Id;
            rentableItemsSettingsVer.Updated_at = rentableItemsSettings.Updated_at;
            rentableItemsSettingsVer.Updated_By_User_Id = rentableItemsSettings.Updated_By_User_Id;
            rentableItemsSettingsVer.Version = rentableItemsSettings.Version;
            rentableItemsSettingsVer.Workflow_state = rentableItemsSettings.Workflow_state;

            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            return rentableItemsSettingsVer;
        }
    }
}
