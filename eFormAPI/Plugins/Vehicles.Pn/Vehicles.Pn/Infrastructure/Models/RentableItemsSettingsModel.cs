using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int EformId { get; set; }

        public void Save(RentableItemsPnDbAnySql _dbContext)
        {
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();

            rentableItemsSettings.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            rentableItemsSettings.Version = Version;
            rentableItemsSettings.Created_at = DateTime.Now;
            rentableItemsSettings.Updated_at = DateTime.Now;
            rentableItemsSettings.Created_By_User_Id = CreatedByUserID;
            rentableItemsSettings.Updated_By_User_Id = UpdatedByUserID;
            rentableItemsSettings.Eform_Id = EformId;

            _dbContext.RentableItemsSettings.Add(rentableItemsSettings);
            _dbContext.SaveChanges();

        }

        public void Update(RentableItemsPnDbAnySql _dbContext)
        {
            RentableItemsSettings rentableItemsSettings = _dbContext.RentableItemsSettings.FirstOrDefault(x => x.Id == Id);

            if(rentableItemsSettings == null)
            {
                throw new NullReferenceException($"Could not find RentableItem Setting with id {Id}");
            }

            rentableItemsSettings.Eform_Id = EformId;
            rentableItemsSettings.Workflow_state = WorkflowState;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemsSettings.Updated_at = DateTime.Now;
                rentableItemsSettings.Updated_By_User_Id = UpdatedByUserID;
                rentableItemsSettings.Version += 1;
                _dbContext.SaveChanges();
                MapRentableItemsSettings(_dbContext, rentableItemsSettings);
            }
        }

        public void Delete(RentableItemsPnDbAnySql _dbContext)
        {
            WorkflowState = eFormShared.Constants.WorkflowStates.Removed;
            Update(_dbContext);
        }
        public void MapRentableItemsSettings(RentableItemsPnDbAnySql _dbContext, RentableItemsSettings rentableItemsSettings)
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

        }
    }
}
