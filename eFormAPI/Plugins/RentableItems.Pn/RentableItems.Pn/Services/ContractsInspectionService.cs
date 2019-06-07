using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eFormCore;
using eFormData;
using eFormShared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Consts;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;
namespace RentableItems.Pn.Services
{
    public class ContractsInspectionService : IContractsInspectionService
    {
        private readonly ILogger<ContractsInspectionService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly RentableItemsPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;

        public ContractsInspectionService(RentableItemsPnDbContext dbContext,
            ILogger<ContractsInspectionService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _coreHelper = coreHelper;
            _rentableItemsLocalizationService = rentableItemLocalizationService;
        }
        public async Task<OperationDataResult<ContractInspectionsModel>> GetAllContractInspections(ContractInspectionsRequestModel contractInspectionsPnRequestModel)
        {
            try
            {
                ContractInspectionsModel contractInspectionsModel = new ContractInspectionsModel();
                IQueryable<ContractInspection> contractInspectionsQuery = _dbContext.ContractInspection.AsQueryable();
                if (!string.IsNullOrEmpty(contractInspectionsPnRequestModel.SortColumnName))
                {
                    if (contractInspectionsPnRequestModel.IsSortDsc)
                    {
                        contractInspectionsQuery = contractInspectionsQuery.CustomOrderByDescending(contractInspectionsPnRequestModel.SortColumnName);
                    }
                    else
                    {
                        contractInspectionsQuery = contractInspectionsQuery.CustomOrderBy(contractInspectionsPnRequestModel.SortColumnName);

                    }
                }
                contractInspectionsModel.Total = await contractInspectionsQuery.CountAsync(x => x.WorkflowState != eFormShared.Constants.WorkflowStates.Removed);
                contractInspectionsQuery
                    = contractInspectionsQuery
                        .Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                        .Skip(contractInspectionsPnRequestModel.Offset)
                        .Take(contractInspectionsPnRequestModel.PageSize);
                List<ContractInspection> contractInspections = contractInspectionsQuery.ToList();
                contractInspections.ForEach(contractInspection =>
                {
                    contractInspectionsModel.ContractInspections.Add(new ContractInspectionModel()
                    {
                        ContractId = contractInspection.ContractId,
                        DoneAt = contractInspection.DoneAt,
                        SdkCaseId = contractInspection.SDK_Case_Id,
                        SiteId = contractInspection.SiteId,
                        Id = contractInspection.Id,
                    });
                });
                return new OperationDataResult<ContractInspectionsModel>(true, contractInspectionsModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<ContractInspectionsModel>(true,
                    _rentableItemsLocalizationService.GetString("ErrorObtainingContractInspectionInfo"));
            }
        }

        public async Task<OperationResult> CreateContractInspection(ContractInspectionModel contractInspectionCreateModel)
        {
            try
            {                
                // finde eform fra settings

                string lookup = $"RentableItemBaseSettings:{RentableItemsSettingsEnum.SdkeFormId.ToString()}";

                LogEvent($"lookup is {lookup}");

                string result = _dbContext.PluginConfigurationValues.AsNoTracking()
                    .FirstOrDefault(x => 
                        x.Name == lookup)
                    ?.Value;
                
                LogEvent($"result is {result}");
                // modificere mainelement

                Contract dbContract =
                    await _dbContext.Contract.FirstOrDefaultAsync(x =>
                        x.Id == contractInspectionCreateModel.ContractId);
                
                
                int eFormId = int.Parse(result);
                Core _core = _coreHelper.GetCore();
                MainElement mainElement = _core.TemplateRead(eFormId);
                mainElement.Repeated = 1;
                mainElement.EndDate = DateTime.Now.AddDays(14).ToUniversalTime();
                mainElement.StartDate = DateTime.Now.ToUniversalTime();
                mainElement.Label = contractInspectionCreateModel.ContractId.ToString();
                CDataValue cDataValue = new CDataValue();

                cDataValue.InderValue = $"<b>Kontrakt Nr:<b>{dbContract.ContractNr.ToString()}<br>";
                cDataValue.InderValue += $"<b>Kunde Nr:<b>{dbContract.CustomerId.ToString()}";
                mainElement.ElementList[0].Description = cDataValue;
                mainElement.ElementList[0].Label = mainElement.Label;
                // finde sites som eform skal sendes til

                List<Site_Dto> sites = new List<Site_Dto>();
                

                lookup = $"RentableItemBaseSettings:{RentableItemsSettingsEnum.EnabledSiteIds.ToString()}";
                LogEvent($"lookup is {lookup}");

                string sdkSiteIds = _dbContext.PluginConfigurationValues.AsNoTracking()
                    .FirstOrDefault(x => 
                        x.Name == lookup)
                    ?.Value;
                
                LogEvent($"sdkSiteIds is {sdkSiteIds}");
                
                foreach (string siteId in sdkSiteIds.Split(","))
                {
                    LogEvent($"found siteId {siteId}");
                    sites.Add(_core.SiteRead(int.Parse(siteId)));
                }

                foreach (Site_Dto siteDto in sites)
                {
                    // sende eform core.caseCreate

                    string sdkCaseId = _core.CaseCreate(mainElement, "", siteDto.SiteId);

                    if (!string.IsNullOrEmpty(sdkCaseId))
                    {
                        // gemme caseid på contractInspectionCreateModel

                        contractInspectionCreateModel.SiteId = siteDto.SiteId;
                        contractInspectionCreateModel.SdkCaseId = sdkCaseId;
                        await contractInspectionCreateModel.Create(_dbContext);
                    }
                }

                
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, _rentableItemsLocalizationService.GetString("ErrorWhileCreatingContractInspection"));
            }
        }

        public async Task<OperationResult> UpdateContractInspection(ContractInspectionModel contractInspectionUpdateModel)
        {
            try
            {
                await contractInspectionUpdateModel.Update(_dbContext);
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingContractInspection"));
            }
        }
        public async Task<OperationResult> DeleteContractInspection(int id)
        {
            ContractInspectionModel contractInspectionDeleteModel = new ContractInspectionModel();
            ContractInspection dbContractInspection =
                await _dbContext.ContractInspection.SingleOrDefaultAsync(x => x.Id == id);

            contractInspectionDeleteModel.Id = dbContractInspection.Id;
            try
            {
                await contractInspectionDeleteModel.Delete(_dbContext);
                return new OperationResult(true);
            }
            catch ( Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileDeletingContractInspection"));
            }
        }
        private void LogEvent(string appendText)
        {
            try
            {                
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[DBG] " + appendText);
                Console.ForegroundColor = oldColor;
            }
            catch
            {
            }
        }
    }
}
