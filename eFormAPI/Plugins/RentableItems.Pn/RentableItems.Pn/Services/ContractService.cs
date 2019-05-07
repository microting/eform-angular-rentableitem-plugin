using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ContractService : IContractsService
    {
        private readonly ILogger<ContractService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly RentableItemsPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;

        public ContractService(RentableItemsPnDbContext dbContext,
            ILogger<ContractService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _coreHelper = coreHelper;
            _rentableItemsLocalizationService = rentableItemLocalizationService;
        }
        public async Task<OperationDataResult<ContractsModel>> GetAllContracts(ContractsRequestModel contractsPnRequestModel)
        {
            try
            {
                ContractsModel contractsModel = new ContractsModel();
                IQueryable<Contract> contractsQuery = _dbContext.Contract.AsQueryable();
                if (!string.IsNullOrEmpty(contractsPnRequestModel.SortColumnName))
                {
                    if (contractsPnRequestModel.IsSortDsc)
                    {
                        contractsQuery = contractsQuery.CustomOrderByDescending(contractsPnRequestModel.SortColumnName);
                    }
                    else
                    {
                        contractsQuery = contractsQuery.CustomOrderBy(contractsPnRequestModel.SortColumnName);

                    }
                }
                contractsModel.Total = contractsQuery.Count();
                contractsQuery 
                    = contractsQuery
                        .Where(x => x.WorkflowState != eFormShared.Constants.WorkflowStates.Removed)
                        .Skip(contractsPnRequestModel.Offset)
                        .Take(contractsPnRequestModel.PageSize);
                List<Contract> contracts = contractsQuery.ToList();
                contracts.ForEach(contract =>
                {
                    contractsModel.Contracts.Add(new ContractModel()
                    {
                        ContractEnd = contract.ContractEnd,
                        ContractNr = contract.ContractNr,
                        ContractStart = contract.ContractStart,
                        CustomerId = contract.CustomerId,
                        Id = contract.Id,

                    });
                });
                return new OperationDataResult<ContractsModel>(true, contractsModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<ContractsModel>(true, 
                    _rentableItemsLocalizationService.GetString("ErrorObtainingRentableItemsInfo"));
            }
        }
            
            

        public async Task<OperationResult> CreateContract(ContractModel contractCreateModel)
        {
            try
            {

                await contractCreateModel.Create(_dbContext);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ContractCreated",
                    contractCreateModel.CustomerId,
                        contractCreateModel.ContractNr));

            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileCreatingContract"));

            }
        }

        public async Task<OperationResult> UpdateContract(ContractModel contractUpdateModel)
        {
            try
            {
                await contractUpdateModel.Update(_dbContext);
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingContract"));
            }
        }

        public async Task<OperationResult> DeleteContract(ContractModel contractDeleteModel)
        {
            try
            {
                await contractDeleteModel.Delete(_dbContext);
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileDeletingContract"));
            }
        }
    }
}
