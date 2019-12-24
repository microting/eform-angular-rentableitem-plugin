using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eFormCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eForm.Dto;
using Microting.eForm.Infrastructure.Constants;
using Microting.eForm.Infrastructure.Models;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Consts;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Services
{
    public class ContractsInspectionService : IContractsInspectionService
    {
        private readonly ILogger<ContractsInspectionService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly eFormRentableItemPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;

        public ContractsInspectionService(eFormRentableItemPnDbContext dbContext,
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
        public async Task<OperationDataResult<ContractInspectionsModel>> Index(ContractInspectionsRequestModel contractInspectionsPnRequestModel)
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
                contractInspectionsModel.Total = await contractInspectionsQuery.CountAsync(x => x.WorkflowState != Constants.WorkflowStates.Removed);
                contractInspectionsQuery
                    = contractInspectionsQuery
                        .Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                        .Skip(contractInspectionsPnRequestModel.Offset)
                        .Take(contractInspectionsPnRequestModel.PageSize);
                List<ContractInspection> contractInspections = contractInspectionsQuery.ToList();
                contractInspections.ForEach(contractInspection =>
                {
                    ContractInspectionItem contractInspectionItem =
                        _dbContext.ContractInspectionItem.FirstOrDefault(x =>
                            x.ContractInspectionId == contractInspection.Id);
                    RentableItem rentableItem =
                        _dbContext.RentableItem.FirstOrDefault(y => y.Id == contractInspectionItem.RentableItemId);
                    contractInspectionsModel.ContractInspections.Add(new ContractInspectionModel()
                    {
                        SdkCaseId = contractInspectionItem.SDKCaseId,
                        eFormId = rentableItem.eFormId,
                        ContractId = contractInspection.ContractId,
                        DoneAt = contractInspection.DoneAt,
                        Id = contractInspection.Id,
                    });
                    
                });
                return new OperationDataResult<ContractInspectionsModel>(true, contractInspectionsModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<ContractInspectionsModel>(false,
                    _rentableItemsLocalizationService.GetString("ErrorObtainingContractInspectionInfo"));
            }
        }

        public async Task<OperationResult> Create(ContractInspectionModel contractInspectionCreateModel)
        {
            try
            {                
                Core _core = await _coreHelper.GetCore();

                // finde eform fra settings
                List<ContractRentableItem> contractRentableItem =
                    await _dbContext.ContractRentableItem.Where(x =>
                        x.ContractId == contractInspectionCreateModel.ContractId).ToListAsync();
                foreach (var item in contractRentableItem)
                {
                    int rentableItemId = item.RentableItemId;

                    RentableItem rentableItem =
                        await _dbContext.RentableItem.FirstOrDefaultAsync(x => x.Id == rentableItemId);
                    int eFormId = rentableItem.eFormId;
                    string lookup = $"RentableItemBaseSettings:{RentableItemsSettingsEnum.EnabledSiteIds.ToString()}";
//                string lookupeForm = $"RentableItemBaseSettings:{RentableItemsSettingsEnum.SdkeFormId.ToString()}";
                    LogEvent($"lookup is {lookup}");
//                LogEvent($"lookupeForm is {lookupeForm}");
                    string result = _dbContext.PluginConfigurationValues.AsNoTracking()
                        .FirstOrDefault(x =>
                            x.Name == lookup)
                        ?.Value;
//                string resulteForm = _dbContext.PluginConfigurationValues.AsNoTracking()
//                    .FirstOrDefault(y => 
//                        y.Name == lookupeForm)
//                    ?.Value;
                    LogEvent($"result is {result}");
//                LogEvent($"resulteForm i {resulteForm}");
                    // modificere mainelement

                    Contract dbContract =
                        await _dbContext.Contract.FirstOrDefaultAsync(x =>
                            x.Id == contractInspectionCreateModel.ContractId);

//                int eFormId = int.Parse(resulteForm);
                    MainElement mainElement = await _core.TemplateRead(eFormId);
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

                    List<SiteDto> sites = new List<SiteDto>();


                    string lookupSite =
                        $"RentableItemBaseSettings:{RentableItemsSettingsEnum.EnabledSiteIds.ToString()}";
                    LogEvent($"lookup is {lookupSite}");

                    string sdkSiteIds = _dbContext.PluginConfigurationValues.AsNoTracking()
                        .FirstOrDefault(x =>
                            x.Name == lookupSite)
                        ?.Value;

                    LogEvent($"sdkSiteIds is {sdkSiteIds}");

                    foreach (string siteId in sdkSiteIds.Split(","))
                    {
                        LogEvent($"found siteId {siteId}");
                        sites.Add(await _core.SiteRead(int.Parse(siteId)));
                    }

                    foreach (SiteDto siteDto in sites)
                    {
                        // sende eform core.caseCreate

                        int? sdkCaseId = await _core.CaseCreate(mainElement, "", siteDto.SiteId);

                        if (sdkCaseId != null)
                        {
                            // gemme caseid på contractInspection
                            ContractInspection contractInspection = new ContractInspection
                            {
                                ContractId = contractInspectionCreateModel.ContractId
                            };
                            await contractInspection.Create(_dbContext);
                            ContractInspectionItem contractInspectionItem = new ContractInspectionItem
                            {
                                ContractInspectionId = contractInspection.Id,
                                RentableItemId = rentableItemId,
                                SiteId = siteDto.SiteId,
                                SDKCaseId = (int) sdkCaseId
                            };
                            await contractInspectionItem.Create(_dbContext);
                        }
                    }
                }

                return new OperationResult(true, "Inspection Created Successfully");
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, _rentableItemsLocalizationService.GetString("ErrorWhileCreatingContractInspection"));
            }
        }

        public async Task<OperationResult> Update(ContractInspectionModel contractInspectionUpdateModel)
        {
            try
            {
                ContractInspection contractInspection =
                    await _dbContext.ContractInspection.SingleOrDefaultAsync(x =>
                        x.Id == contractInspectionUpdateModel.Id);
                if (contractInspection != null)
                {
                    contractInspection.ContractId = contractInspectionUpdateModel.ContractId;
                    contractInspection.DoneAt = contractInspectionUpdateModel.DoneAt;
//                    contractInspection.SiteId = contractInspectionUpdateModel.SiteId;
//                    contractInspection.SDKCaseId = contractInspectionUpdateModel.SdkCaseId;
                    await contractInspection.Update(_dbContext);
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingContractInspection"));
            }
        }
        public async Task<OperationResult> Delete(int id)
        {
            ContractInspection dbContractInspection =
                await _dbContext.ContractInspection.SingleOrDefaultAsync(x => x.Id == id);

            try
            {
                if (dbContractInspection != null)
                {
                    await dbContractInspection.Delete(_dbContext);
                }
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
