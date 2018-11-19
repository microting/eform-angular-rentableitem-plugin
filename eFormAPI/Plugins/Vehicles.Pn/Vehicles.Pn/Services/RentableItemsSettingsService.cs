using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using eFormCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Services
{
    public class RentableItemsSettingsService : IRentableItemsSettingsService
    {
        private readonly ILogger<RentableItemsSettingsService> _logger;
        private readonly IRentableItemsLocalizationService _rentablteItemsLocalizationsService;
        private readonly RentableItemsPnDbAnySql _dbContext;
        private readonly IEFormCoreService _coreHelper;

        public RentableItemsSettingsService(ILogger<RentableItemsSettingsService> logger,
            RentableItemsPnDbAnySql dbContext,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemsLocalizationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _rentablteItemsLocalizationsService = rentableItemsLocalizationService;
        }

        public OperationDataResult<RentableItemsSettingsModel> GetSettings()
        {
            try
            {
                RentableItemsSettingsModel result = new RentableItemsSettingsModel();
                RentableItemsSettings rentableItemsSettings = _dbContext.RentableItemsSettings.FirstOrDefault();
                if(rentableItemsSettings?.Eform_Id != null)
                {
                    result.EformId = (int)rentableItemsSettings.Eform_Id;
                    
                }
                else
                {
                    result.EformId = null;
                }
                return new OperationDataResult<RentableItemsSettingsModel>(true, result);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemsSettingsModel>(false,
                    _rentablteItemsLocalizationsService.GetString("ErrorWhileObtainingRentableItemsSettings"));
            }
        }

        public OperationResult UpdateSettings(RentableItemsSettingsModel rentableItemsSettingsModel)
        {
            try
            {
                if(rentableItemsSettingsModel.EformId == 0)
                {
                    return new OperationResult(true);
                }
                RentableItemsSettings rentableItemsSettings = _dbContext.RentableItemsSettings.FirstOrDefault();
                if(rentableItemsSettings == null)
                {
                    rentableItemsSettings = new RentableItemsSettings()
                    {
                        Eform_Id = rentableItemsSettingsModel.EformId
                    };
                    _dbContext.RentableItemsSettings.Add(rentableItemsSettings);
                }
                else
                {
                    rentableItemsSettings.Eform_Id = rentableItemsSettingsModel.EformId;
                }
                _dbContext.SaveChanges();
                return new OperationResult(true,
                    _rentablteItemsLocalizationsService.GetString("SettingsHasBeenUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false,
                    _rentablteItemsLocalizationsService.GetString("ErrorWhileUpdatingRentableItemsSettings"));
            }

        }
    }
}
