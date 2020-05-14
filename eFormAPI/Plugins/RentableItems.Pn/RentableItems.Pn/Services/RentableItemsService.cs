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
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;
using CollectionExtensions = Castle.Core.Internal.CollectionExtensions;

//using Customers.Pn.Infrastructure.Models.Customer;

namespace RentableItems.Pn.Services
{
    public class RentableItemsService : IRentableItemsService
    {
            private readonly ILogger<RentableItemsService> _logger;
            private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
            private readonly eFormRentableItemPnDbContext _dbContext;
            private readonly IEFormCoreService _coreHelper;

            public RentableItemsService(eFormRentableItemPnDbContext dbContext,
                ILogger<RentableItemsService> logger, 
                IEFormCoreService coreHelper,
                IRentableItemsLocalizationService rentableItemLocalizationService)
            {
                _dbContext = dbContext;
                _logger = logger;
                _coreHelper = coreHelper;
                _rentableItemsLocalizationService = rentableItemLocalizationService;
            }

        public async Task<OperationDataResult<RentableItemsModel>> Index(RentableItemsRequestModel pnRequestModel)
        {
            try
            {
                RentableItemsModel rentableItemsPnModel = new RentableItemsModel();
                
                IQueryable<RentableItem> rentableItemsQuery = _dbContext.RentableItem.AsQueryable();
                if (!CollectionExtensions.IsNullOrEmpty(pnRequestModel.NameFilter) && pnRequestModel.NameFilter != "")
                {
                    rentableItemsQuery = rentableItemsQuery.Where(x =>
                        x.Brand.ToLower().Contains(pnRequestModel.NameFilter.ToLower()) ||
                        x.VinNumber.ToLower().Contains(pnRequestModel.NameFilter.ToLower()) ||
                        x.SerialNumber.ToLower().Contains(pnRequestModel.NameFilter.ToLower()) ||
                        x.PlateNumber.ToLower().Contains(pnRequestModel.NameFilter.ToLower()) ||
                        x.ModelName.ToLower().Contains(pnRequestModel.NameFilter.ToLower()));
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
                        EFormId = rentableItem.eFormId
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

        public async Task<OperationResult> Create(RentableItemModel rentableItemPnCreateModel)
        {
            try
            {
                RentableItem rentableItem = new RentableItem
                {
                    Brand = rentableItemPnCreateModel.Brand,
                    ModelName = rentableItemPnCreateModel.ModelName,
                    RegistrationDate = rentableItemPnCreateModel.RegistrationDate,
                    VinNumber = rentableItemPnCreateModel.VinNumber,
                    SerialNumber = rentableItemPnCreateModel.SerialNumber,
                    PlateNumber = rentableItemPnCreateModel.PlateNumber,
                    eFormId = rentableItemPnCreateModel.EFormId
                };

                await rentableItem.Create(_dbContext);

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

        public async Task<OperationResult> Update(RentableItemModel rentableItemPnUpdateModel)
        {
            try
            {
                RentableItem rentableItem = await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.Id == rentableItemPnUpdateModel.Id);

                if (rentableItem != null)
                {
                    rentableItem.Brand = rentableItemPnUpdateModel.Brand;
                    rentableItem.ModelName = rentableItemPnUpdateModel.ModelName;
                    rentableItem.RegistrationDate = rentableItemPnUpdateModel.RegistrationDate;
                    rentableItem.SerialNumber = rentableItemPnUpdateModel.SerialNumber;
                    rentableItem.VinNumber = rentableItemPnUpdateModel.VinNumber;
                    rentableItem.PlateNumber = rentableItemPnUpdateModel.PlateNumber;
                    rentableItem.eFormId = rentableItemPnUpdateModel.EFormId;
                
                    await rentableItem.Update(_dbContext);
                }
                return new OperationDataResult<RentableItemsModel>(true, 
                    _rentableItemsLocalizationService.GetString("RentableItemUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemsModel>(true,
                    _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingRentableItemInfo"));
            }
        }
        public async Task<OperationResult> Delete(int id)
        {
            RentableItem rentableItem = await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.Id == id);

            try
            {
                if (rentableItem != null)
                {
                    await rentableItem.Delete(_dbContext);
                }
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