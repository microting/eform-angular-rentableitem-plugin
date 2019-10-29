﻿using System;
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
                contractInspectionsModel.Total = await contractInspectionsQuery.CountAsync(x => x.WorkflowState != Constants.WorkflowStates.Removed);
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
                        SdkCaseId = contractInspection.SDKCaseId,
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

                string lookup = $"RentableItemBaseSettings:{RentableItemsSettingsEnum.EnabledSiteIds.ToString()}";
                string lookupeForm = $"RentableItemBaseSettings:{RentableItemsSettingsEnum.SdkeFormId.ToString()}";
                LogEvent($"lookup is {lookup}");
                LogEvent($"lookupeForm is {lookupeForm}");
                string result = _dbContext.PluginConfigurationValues.AsNoTracking()
                    .FirstOrDefault(x => 
                        x.Name == lookup)
                    ?.Value;
                string resulteForm = _dbContext.PluginConfigurationValues.AsNoTracking()
                    .FirstOrDefault(y => 
                        y.Name == lookupeForm)
                    ?.Value;
                LogEvent($"result is {result}");
                LogEvent($"resuleForm i {resulteForm}");
                // modificere mainelement

                Contract dbContract =
                    await _dbContext.Contract.FirstOrDefaultAsync(x =>
                        x.Id == contractInspectionCreateModel.ContractId);
                
                
                int eFormId = int.Parse(resulteForm);
                Core _core = _coreHelper.GetCore();
                MainElement mainElement = _core.TemplateRead(eFormId);
                mainElement.Repeated = 1;
                mainElement.EndDate = DateTime.Now.AddDays(14).ToUniversalTime();//why 14 days?
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

                    int? sdkCaseId = _core.CaseCreate(mainElement, "", siteDto.SiteId);

                    if (sdkCaseId != null)
                    {
                        // gemme caseid på contractInspection
                        ContractInspection contractInspection = new ContractInspection
                        {
                            SiteId = siteDto.SiteId,
                            SDKCaseId = (int) sdkCaseId
                        };
                        await contractInspection.Create(_dbContext);
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

        public async Task<OperationResult> UpdateContractInspection(ContractInspectionModel contractInspectionUpdateModel)
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
                    contractInspection.SiteId = contractInspectionUpdateModel.SiteId;
                    contractInspection.SDKCaseId = contractInspectionUpdateModel.SdkCaseId;
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
        public async Task<OperationResult> DeleteContractInspection(int id)
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
