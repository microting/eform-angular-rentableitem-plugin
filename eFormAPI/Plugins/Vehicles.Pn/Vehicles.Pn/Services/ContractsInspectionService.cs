using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Models;
namespace RentableItems.Pn.Services
{
    public class ContractsInspectionService : IContractsInspectionService
    {
        private readonly ILogger<ContractsInspectionService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly RentableItemsPnDbAnySql _dbContext;
        private readonly IEFormCoreService _coreHelper;

        public ContractsInspectionService(RentableItemsPnDbAnySql dbContext,
            ILogger<ContractsInspectionService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _coreHelper = coreHelper;
            _rentableItemsLocalizationService = rentableItemLocalizationService;
        }
        public OperationDataResult<ContractInspectionsModel> GetAllContractInspections(ContractInspectionsRequestModel contractInspectionsPnRequestModel)
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
                contractInspectionsModel.Total = contractInspectionsQuery.Count();
                contractInspectionsQuery = contractInspectionsQuery.Skip(contractInspectionsPnRequestModel.Offset).Take(contractInspectionsPnRequestModel.PageSize);
                List<ContractInspection> contractInspections = contractInspectionsQuery.ToList();
                contractInspections.ForEach(contractInspection =>
                {
                    contractInspectionsModel.ContractInspections.Add(new ContractInspectionModel()
                    {
                        ContractId = contractInspection.ContractId,
                        DoneAt = contractInspection.DoneAt,
                        SdkCaseId = contractInspection.SDK_Case_Id,
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

        public OperationResult CreateContractInspection(ContractInspectionModel contractInspectionCreateModel)
        {
            try
            {
                contractInspectionCreateModel. Save(_dbContext);
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileCreatingContractInspection"));
            }
        }

        public OperationResult UpdateContractInspection(ContractInspectionModel contractInspectionUpdateModel)
        {
            try
            {
                contractInspectionUpdateModel.Update(_dbContext);
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingContractInspection"));
            }
        }
        public OperationResult DeleteContractInspection(ContractInspectionModel contractInspectionDeleteModel)
        {
            try
            {
                contractInspectionDeleteModel.Delete(_dbContext);
                return new OperationResult(true);
            }
            catch ( Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileDeletingContractInspection"));
            }
        }
    }
}
