using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;
using CollectionExtensions = Castle.Core.Internal.CollectionExtensions;

//using Customers.Pn.Infrastructure.Models.Customer;

namespace RentableItems.Pn.Services
{
    public class RentableItemsService : IRentableItemsService
    {
            private readonly ILogger<RentableItemsService> _logger;
            private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
            private readonly RentableItemsPnDbContext _dbContext;
            private readonly IEFormCoreService _coreHelper;

            public RentableItemsService(RentableItemsPnDbContext dbContext,
                ILogger<RentableItemsService> logger, 
                IEFormCoreService coreHelper,
                IRentableItemsLocalizationService rentableItemLocalizationService)
            {
                _dbContext = dbContext;
                _logger = logger;
                _coreHelper = coreHelper;
                _rentableItemsLocalizationService = rentableItemLocalizationService;
            }

        public async Task<OperationDataResult<RentableItemsModel>> GetAllRentableItems(RentableItemsRequestModel pnRequestModel)
        {
            try
            {
                RentableItemsModel rentableItemsPnModel = new RentableItemsModel();
                
                IQueryable<RentableItem> rentableItemsQuery = _dbContext.RentableItem.AsQueryable();
                if (!CollectionExtensions.IsNullOrEmpty(pnRequestModel.NameFilter) && pnRequestModel.NameFilter != "")
                {
                    rentableItemsQuery = rentableItemsQuery.Where(x =>
                        x.Brand.Contains(pnRequestModel.NameFilter) ||
                        x.ModelName.Contains(pnRequestModel.NameFilter));
                }
                if (!string.IsNullOrEmpty(pnRequestModel.Sort))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        rentableItemsQuery = rentableItemsQuery.CustomOrderByDescending(pnRequestModel.Sort);
                    }
                    else
                    {
                        rentableItemsQuery = rentableItemsQuery.CustomOrderBy(pnRequestModel.Sort);
                    }
                }

                rentableItemsPnModel.Total = rentableItemsQuery.Count(x => x.WorkflowState != Constants.WorkflowStates.Removed);
                rentableItemsQuery 
                    = rentableItemsQuery
                        .Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                        .Skip(pnRequestModel.Offset)
                        .Take(pnRequestModel.PageSize);
                List<RentableItem> rentableItems = await rentableItemsQuery.ToListAsync();
                rentableItems.ForEach(rentableItem =>
                {
                    rentableItemsPnModel.RentableItems.Add(new RentableItemModel()
                    {
                        VinNumber = rentableItem.VinNumber,
                        RegistrationDate = rentableItem.RegistrationDate,
                        Brand = rentableItem.Brand,
                        PlateNumber = rentableItem.PlateNumber,
                        ModelName = rentableItem.ModelName,
                        SerialNumber = rentableItem.SerialNumber,
                        Id = rentableItem.Id,
                    });
                });
                return new OperationDataResult<RentableItemsModel>(true, rentableItemsPnModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemsModel>(true,
                    _rentableItemsLocalizationService.GetString("ErrorObtainingRentableItemsInfo"));
            }
        }

        public async Task<OperationResult> CreateRentableItem(RentableItemModel rentableItemPnCreateModel)
        {
            try
            {
                //RentableItemModel rentableItemModel = new RentableItemModel(rentableItemPnCreateModel.Id, rentableItemPnCreateModel.Brand,
                //    rentableItemPnCreateModel.ModelName, rentableItemPnCreateModel.RegistrationDate, rentableItemPnCreateModel.VinNumber,
                //    rentableItemPnCreateModel.SerialNumber, rentableItemPnCreateModel.PlateNumber);

                //rentableItemPn.VinNumber = rentableItemPnCreateModel.VinNumber;
                //rentableItemPn.Brand = rentableItemPnCreateModel.Brand;
                //rentableItemPn.SerialNumber = rentableItemPnCreateModel.SerialNumber;
                //rentableItemPn.PlateNumber = rentableItemPnCreateModel.PlateNumber;
                //rentableItemPn.ModelName = rentableItemPnCreateModel.ModelName;
                //rentableItemPn.RegistrationDate = rentableItemPnCreateModel.RegistrationDate;

                await rentableItemPnCreateModel.Create(_dbContext);

                //_dbContext.RentableItem.Add(rentableItemPn);
                //_dbContext.SaveChanges();
                return new OperationResult(true,
                    _rentableItemsLocalizationService.GetString("RentableItemCreated", rentableItemPnCreateModel.Brand,
                        rentableItemPnCreateModel.ModelName));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileCreatingRentableItem"));
            }
        }

        public async Task<OperationResult> UpdateRentableItem(RentableItemModel rentableItemPnUpdateModel)
        {
            try
            {
                //    var rentableItem = _dbContext.RentableItem.FirstOrDefault(x => x.Id == rentableItemPnUpdateModel.Id);
                //    if (rentableItem == null)
                //    {
                //        return new OperationResult(true, "Rentable Item not found");
                //    }

                //    rentableItem.VinNumber = rentableItemPnUpdateModel.VinNumber;
                //    rentableItem.Brand = rentableItemPnUpdateModel.Brand;
                //    rentableItem.PlateNumber = rentableItemPnUpdateModel.PlateNumber;
                //    rentableItem.SerialNumber = rentableItemPnUpdateModel.SerialNumber;
                //    rentableItem.ModelName = rentableItemPnUpdateModel.ModelName;
                //    rentableItem.RegistrationDate = rentableItemPnUpdateModel.RegistrationDate;

                await rentableItemPnUpdateModel.Update(_dbContext);
                return new OperationDataResult<RentableItemsModel>(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemsModel>(true,
                    _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingRentableItemInfo"));
            }
        }
        public async Task<OperationResult> DeleteRentableItem(int id)
        {
            RentableItem dbRentableItem = await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.Id == id);
            RentableItemModel rentableItemPnDeleteModel = new RentableItemModel();

            rentableItemPnDeleteModel.Id = dbRentableItem.Id;
            try
            {
                await rentableItemPnDeleteModel.Delete(_dbContext);
                return new OperationDataResult<RentableItemsModel>(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemsModel>(true,
                    _rentableItemsLocalizationService.GetString("ErrorWhileDeletingRentableItem"));
            }
        }
    }
}