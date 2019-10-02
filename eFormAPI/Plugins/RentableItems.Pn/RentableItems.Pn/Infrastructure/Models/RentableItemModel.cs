using System;
using System.Linq;
using System.Threading.Tasks;
using Microting.eForm.Infrastructure.Constants;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemModel : IModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public string VinNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PlateNumber { get; set; }
        public string WorkflowState { get; set; }

        //public RentableItemModel(int? id, string brand, string modelName, DateTime registrationDate, string vinNumber, string serialNumber, string plateNumber)
        //{
        //    //this.Id = id;
        //    this.Brand = brand;
        //    this.ModelName = modelName;
        //    this.RegistrationDate = registrationDate;
        //    this.VinNumber = vinNumber;
        //    this.SerialNumber = serialNumber;
        //    this.PlateNumber = plateNumber;
        //}

        public async Task Create(RentableItemsPnDbContext _dbContext)
        {
            
            RentableItem rentableItem = new RentableItem();
            rentableItem.VinNumber = VinNumber;
            rentableItem.Brand = Brand;
            rentableItem.SerialNumber = SerialNumber;
            rentableItem.PlateNumber = PlateNumber;
            rentableItem.ModelName = ModelName;
            rentableItem.RegistrationDate = RegistrationDate;
            rentableItem.WorkflowState = Constants.WorkflowStates.Created;
            rentableItem.CreatedAt = DateTime.Now;
            rentableItem.UpdatedAt = DateTime.Now;
            rentableItem.CreatedByUserId = CreatedByUserId;
            rentableItem.UpdatedByUserId = UpdatedByUserId;
            _dbContext.RentableItem.Add(rentableItem);
            await _dbContext.SaveChangesAsync();

            _dbContext.RentableItemsVersion.Add(MapRentableItemVersions(_dbContext, rentableItem));
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(RentableItemsPnDbContext _dbContext)
        {
            RentableItem rentableItem = _dbContext.RentableItem.FirstOrDefault(x => x.Id == Id);

            if (rentableItem == null)
            {
                throw new NullReferenceException($"Could not find RentableItem with id {Id}");
            }

            rentableItem.Brand = Brand;
            rentableItem.ModelName = ModelName;
            rentableItem.RegistrationDate = RegistrationDate;
            rentableItem.VinNumber = VinNumber;
            rentableItem.SerialNumber = SerialNumber;
            rentableItem.PlateNumber = PlateNumber;
            rentableItem.WorkflowState = rentableItem.WorkflowState;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItem.UpdatedAt = DateTime.Now;
                rentableItem.UpdatedByUserId = UpdatedByUserId;
                rentableItem.Version += 1;

                _dbContext.RentableItemsVersion.Add(MapRentableItemVersions(_dbContext, rentableItem));
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task Delete(RentableItemsPnDbContext _dbContext)
        {
            RentableItem rentableItem = _dbContext.RentableItem.FirstOrDefault(x => x.Id == Id);

            if (rentableItem == null)
            {
                throw new NullReferenceException($"Could not find RentableItem with id {Id}");
            }

            rentableItem.WorkflowState = Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                rentableItem.UpdatedAt = DateTime.Now;
                rentableItem.UpdatedByUserId = UpdatedByUserId;
                rentableItem.Version += 1;

                _dbContext.RentableItemsVersion.Add(MapRentableItemVersions(_dbContext, rentableItem));
                await _dbContext.SaveChangesAsync();
            }
        }

        private RentableItemsVersions MapRentableItemVersions(RentableItemsPnDbContext _dbContext, RentableItem rentableItem)
        {
            RentableItemsVersions rentableItemVer = new RentableItemsVersions();

            rentableItemVer.Brand = rentableItem.Brand;
            rentableItemVer.CreatedAt = rentableItem.CreatedAt;
            rentableItemVer.CreatedByUserId = rentableItem.CreatedByUserId;
            rentableItemVer.ModelName = rentableItem.ModelName;
            rentableItemVer.PlateNumber = rentableItem.PlateNumber;
            rentableItemVer.RegistrationDate = rentableItem.RegistrationDate;
            rentableItemVer.SerialNumber = rentableItem.SerialNumber;
            rentableItemVer.UpdatedAt = rentableItem.UpdatedAt;
            rentableItemVer.UpdatedByUserId = rentableItem.UpdatedByUserId;
            rentableItemVer.Version = rentableItem.Version;
            rentableItemVer.VinNumber = rentableItem.VinNumber;
            rentableItemVer.WorkflowState = rentableItem.WorkflowState;

            rentableItemVer.RentableItemId = rentableItem.Id;

            return rentableItemVer;
        }

    }

}
