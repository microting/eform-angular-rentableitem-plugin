using System;
using System.Linq;
using System.Threading.Tasks;
using Microting.eForm.Infrastructure.Constants;
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
        
        public async Task Create(RentableItemsPnDbContext _dbContext)
        {
            RentableItemsSettings rentableItemsSettings = new RentableItemsSettings();

            rentableItemsSettings.WorkflowState = Constants.WorkflowStates.Created;
            rentableItemsSettings.Version = Version;
            rentableItemsSettings.CreatedAt = DateTime.Now;
            rentableItemsSettings.UpdatedAt = DateTime.Now;
            rentableItemsSettings.CreatedByUserId = CreatedByUserID;
            rentableItemsSettings.UpdatedByUserId = UpdatedByUserID;
            rentableItemsSettings.eFormId = eFormId;

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

            rentableItemsSettings.eFormId = eFormId;
            rentableItemsSettings.WorkflowState = rentableItemsSettings.WorkflowState;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemsSettings.UpdatedAt = DateTime.Now;
                rentableItemsSettings.UpdatedByUserId = UpdatedByUserID;
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

            rentableItemsSettings.WorkflowState = Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItemsSettings.UpdatedAt = DateTime.Now;
                rentableItemsSettings.UpdatedByUserId = UpdatedByUserID;
                rentableItemsSettings.Version += 1;

                _dbContext.RentableItemsSettingsVersions.Add(MapRentableItemsSettings(_dbContext, rentableItemsSettings));
               await _dbContext.SaveChangesAsync();

            }
        }
        public RentableItemsSettingsVersions MapRentableItemsSettings(RentableItemsPnDbContext _dbContext, RentableItemsSettings rentableItemsSettings)
        {
            RentableItemsSettingsVersions rentableItemsSettingsVer = new RentableItemsSettingsVersions();

            rentableItemsSettingsVer.CreatedAt = rentableItemsSettings.CreatedAt;
            rentableItemsSettingsVer.CreatedByUserId = rentableItemsSettings.CreatedByUserId;
            rentableItemsSettingsVer.eFormId = (int)rentableItemsSettings.eFormId;
            rentableItemsSettingsVer.UpdatedAt = rentableItemsSettings.UpdatedAt;
            rentableItemsSettingsVer.UpdatedByUserId = rentableItemsSettings.UpdatedByUserId;
            rentableItemsSettingsVer.Version = rentableItemsSettings.Version;
            rentableItemsSettingsVer.WorkflowState = rentableItemsSettings.WorkflowState;

            rentableItemsSettingsVer.RentableItemsSettingId = rentableItemsSettings.Id;

            return rentableItemsSettingsVer;
        }
    }
}
