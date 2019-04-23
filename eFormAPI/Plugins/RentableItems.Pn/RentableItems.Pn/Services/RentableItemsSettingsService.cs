using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eFormCore;
using eFormData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Helpers.PluginDbOptions;
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
        private readonly RentableItemsPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;
        private readonly IPluginDbOptions<RentableItemBaseSettings> _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RentableItemsSettingsService(ILogger<RentableItemsSettingsService> logger,
            RentableItemsPnDbContext dbContext,
            IEFormCoreService coreHelper,
            IPluginDbOptions<RentableItemBaseSettings> options,
            IRentableItemsLocalizationService rentableItemsLocalizationService,           
            IHttpContextAccessor httpContextAccessor)
            
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _options = options;
            _httpContextAccessor = httpContextAccessor;
            _rentablteItemsLocalizationsService = rentableItemsLocalizationService;
        }

        public async Task<OperationDataResult<RentableItemBaseSettings>> GetSettings()
        {
            try
            {
                RentableItemBaseSettings option = _options.Value;
                
                if (option.SdkConnectionString == "...")
                {
                    string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

                    string dbNameSection = Regex.Match(connectionString, @"(Database=(...)_eform-angular-\w*-plugin;)").Groups[0].Value;
                    string dbPrefix = Regex.Match(connectionString, @"Database=(\d*)_").Groups[1].Value;
                    string sdk = $"Database={dbPrefix}_SDK;";
                    connectionString = connectionString.Replace(dbNameSection, sdk);
                    await _options.UpdateDb(settings => { settings.SdkConnectionString = connectionString;}, _dbContext, UserId);

                }
                
                return new OperationDataResult<RentableItemBaseSettings>(true, option);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemBaseSettings>(false,
                    _rentablteItemsLocalizationsService.GetString("ErrorWhileObtainingRentableItemsSettings"));
            }
        }

        public async Task<OperationResult> UpdateSettings(RentableItemBaseSettings rentableItemsSettingsModel)
        {
            try
            {
                await _options.UpdateDb(settings =>
                    {
                        settings.LogLevel = rentableItemsSettingsModel.LogLevel;
                        settings.LogLimit = rentableItemsSettingsModel.LogLimit;
                        settings.MaxParallelism = rentableItemsSettingsModel.MaxParallelism;
                        settings.NumberOfWorkers = rentableItemsSettingsModel.NumberOfWorkers;
                        settings.SdkConnectionString = rentableItemsSettingsModel.SdkConnectionString;
                        settings.SdkeFormId = rentableItemsSettingsModel.SdkeFormId;
                    }, _dbContext, UserId
                );
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
        public int UserId
        {
            get
            {
                var value = _httpContextAccessor?.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return value == null ? 0 : int.Parse(value);
            }
        }
    }
}
