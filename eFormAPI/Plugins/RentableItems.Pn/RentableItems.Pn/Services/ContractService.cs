﻿using System;
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
using RentableItems.Pn.Infrastructure.Models;
using RentableItems.Pn.Infrastructure.Models.Customer;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Services
{
    public class ContractService : IContractsService
    {
        private readonly ILogger<ContractService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly eFormRentableItemPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;
        private readonly CustomersPnDbAnySql _customerDbContext;


        public ContractService(eFormRentableItemPnDbContext dbContext,
            ILogger<ContractService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService,
            CustomersPnDbAnySql customerDbContext
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _coreHelper = coreHelper;
            _rentableItemsLocalizationService = rentableItemLocalizationService;
            _customerDbContext = customerDbContext;

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
                        .Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                        .Skip(contractsPnRequestModel.Offset)
                        .Take(contractsPnRequestModel.PageSize);
                List<Contract> contracts = await contractsQuery.ToListAsync();
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
                Contract contract =
                    _dbContext.Contract.FirstOrDefault(x => x.ContractNr == contractCreateModel.ContractNr && x.WorkflowState == Constants.WorkflowStates.Created);
                if (contract == null)
                {
                    Contract newContract = new Contract
                    {
                        ContractEnd = contractCreateModel.ContractEnd,
                        ContractNr = contractCreateModel.ContractNr,
                        ContractStart = contractCreateModel.ContractStart,
                        CustomerId = contractCreateModel.CustomerId,
                    };
                    await newContract.Create(_dbContext);


                    foreach (var rentableItemId in contractCreateModel.RentableItemIds)
                    {
                        Contract dbContract =
                            _dbContext.Contract.FirstOrDefault(x => x.ContractNr == contractCreateModel.ContractNr);
                        ContractRentableItem contractRentableItem = new ContractRentableItem();
                        contractRentableItem.RentableItemId = rentableItemId;
                        contractRentableItem.ContractId = dbContract.Id;
                        
                        await contractRentableItem.Create(_dbContext);
                    }
                }

                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ContractCreated",
                    contractCreateModel.CustomerId,
                        contractCreateModel.ContractNr));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, _rentableItemsLocalizationService.GetString("ErrorWhileCreatingContract"));
            }
        }

        public async Task<OperationResult> UpdateContract(ContractModel updateModel)
        {
            try
            {
                Contract contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.Id == updateModel.Id);
                if (contract != null)
                {
                    contract.ContractEnd = updateModel.ContractEnd;
                    contract.ContractNr = updateModel.ContractNr;
                    contract.ContractStart = updateModel.ContractStart;
                    contract.CustomerId = updateModel.CustomerId;

                    await contract.Update(_dbContext);
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingContract"));
            }
        }

        public async Task<OperationResult> DeleteContract(int id)
        {
            Contract contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.Id == id);
            try
            {
                if (contract != null)
                {
                    await contract.Delete(_dbContext);
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ErrorWhileDeletingContract"));
            }
        }
        
        public async Task<OperationDataResult<CustomersModel>> GetAllCustomers(
            CustomersRequestModel pnRequestModel)
        {
            try
            {

                CustomersModel customersPnModel = new CustomersModel();
                CustomerModel customerModel = new CustomerModel();
                IQueryable<Customer> customersQuery = _customerDbContext.Customers.AsQueryable();
                if (!string.IsNullOrEmpty(pnRequestModel.SortColumnName))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        customersQuery = customersQuery
                            .CustomOrderByDescending(pnRequestModel.SortColumnName);
                    }
                    else
                    {
                        customersQuery = customersQuery
                            .CustomOrderBy(pnRequestModel.SortColumnName);
                    }
                }
                else
                {
                    customersQuery = _customerDbContext.Customers
                        .OrderBy(x => x.Id);
                }

                if (!string.IsNullOrEmpty(pnRequestModel.Name))
                {
                    customersQuery = customersQuery.Where(x => x.CompanyName.Contains(pnRequestModel.Name));
				}
                else
                {
                    throw new NullReferenceException("No search criteria were given.");
                }

				customersQuery =
					customersQuery.Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                    .Skip(pnRequestModel.Offset)
                    .Take(pnRequestModel.PageSize);

                List<CustomerModel> customers = await customersQuery.Select(x => new CustomerModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Email = x.Email,
                    ContactPerson = x.ContactPerson,
                    CompanyName = x.CompanyName,
                    Phone = x.Phone,
                    CityName = x.CityName,
                    CompanyAddress = x.CompanyAddress,
                    CompanyAddress2 = x.CompanyAddress2,
                    CountryCode = x.CountryCode,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    CustomerNo = x.CustomerNo,
                    EanCode = x.EanCode,
                    VatNumber = x.VatNumber,
                    ZipCode = x.ZipCode

                }).ToListAsync();
                customersPnModel.Total = await _customerDbContext.Customers.CountAsync(x => x.WorkflowState != Constants.WorkflowStates.Removed);
                customersPnModel.Customers = customers;
                return new OperationDataResult<CustomersModel>(true, customersPnModel);

            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(true, 
                    _rentableItemsLocalizationService.GetString("ErrorObtainingCustomersInfo"));
            }
        }
        
    }
}
