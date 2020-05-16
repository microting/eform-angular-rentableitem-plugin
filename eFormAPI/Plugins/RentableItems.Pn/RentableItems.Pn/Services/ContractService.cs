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
            CustomersPnDbAnySql customerDbContext,
            ILogger<ContractService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService
            )
        {
            _dbContext = dbContext;
            _customerDbContext = customerDbContext;
            _logger = logger;
            _coreHelper = coreHelper;
            _rentableItemsLocalizationService = rentableItemLocalizationService;

        }
        public async Task<OperationDataResult<ContractsModel>> Index(ContractsRequestModel contractsPnRequestModel)
        {
            try
            {
                ContractsModel contractsModel = new ContractsModel();
                IQueryable<Contract> contractsQuery = _dbContext.Contract.
                    Where(x=> x.WorkflowState != Constants.WorkflowStates.Removed).AsQueryable();
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
                    var customer =
                         _customerDbContext.Customers.Single(x => x.Id == contract.CustomerId);
                    RentableItemCustomerModel rentableItemCustomerModel = new RentableItemCustomerModel()
                    {
                        Id = customer.Id,
                        CustomerNo = customer.CustomerNo,
                        CompanyName = customer.CompanyName,
                        ContactPerson = customer.ContactPerson,
                        CompanyAddress = customer.CompanyAddress,
                        CompanyAddress2 = customer.CompanyAddress2,
                        CityName = customer.CityName,
                        ZipCode = customer.ZipCode,
                        CountryCode = customer.CountryCode,
                        EanCode = customer.EanCode,
                        VatNumber = customer.VatNumber,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Description = customer.Description
                    };

                    List<RentableItemModel> rentableItemModels = new List<RentableItemModel>();
                    foreach (ContractRentableItem contractRentableItem in _dbContext.ContractRentableItem.Where(x => x.ContractId == contract.Id && x.WorkflowState == Constants.WorkflowStates.Created).ToList())
                    {
                        RentableItem rentableItem = _dbContext.RentableItem.Single(x => x.Id == contractRentableItem.RentableItemId);
                        RentableItemModel rentableItemModel = new RentableItemModel()
                        {
                            Id = rentableItem.Id,
                            Brand = rentableItem.Brand,
                            ModelName = rentableItem.ModelName,
                            PlateNumber = rentableItem.PlateNumber,
                            VinNumber = rentableItem.VinNumber,
                            SerialNumber = rentableItem.SerialNumber,
                            RegistrationDate = rentableItem.RegistrationDate
                        };
                        rentableItemModels.Add(rentableItemModel);
                    }

                    contractsModel.Contracts.Add(new ContractModel()
                    {
                        ContractEnd = contract.ContractEnd,
                        ContractNr = contract.ContractNr,
                        ContractStart = contract.ContractStart,
                        CustomerId = contract.CustomerId,
                        RentableItemCustomer = rentableItemCustomerModel,
                        RentableItems = rentableItemModels,
                        Id = contract.Id,
                    });
                });
               return new OperationDataResult<ContractsModel>(true, contractsModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<ContractsModel>(false, 
                    _rentableItemsLocalizationService.GetString("ErrorObtainingContracts"));
            }
        }
    //     public async Task<OperationDataResult<CustomersModel>> IndexCustomers(
    //         CustomersRequestModel pnRequestModel)
    //     {
    //         try
    //         {
    //
    //             CustomersModel customersPnModel = new CustomersModel();
    //             CustomerModel customerModel = new CustomerModel();
    //             IQueryable<Customer> customersQuery = _customerDbContext.Customers.AsQueryable();
    //             if (!string.IsNullOrEmpty(pnRequestModel.SortColumnName))
    //             {
    //                 if (pnRequestModel.IsSortDsc)
    //                 {
    //                     customersQuery = customersQuery
    //                         .CustomOrderByDescending(pnRequestModel.SortColumnName);
    //                 }
    //                 else
    //                 {
    //                     customersQuery = customersQuery
    //                         .CustomOrderBy(pnRequestModel.SortColumnName);
    //                 }
    //             }
    //             else
    //             {
    //                 customersQuery = _customerDbContext.Customers
    //                     .OrderBy(x => x.Id);
    //             }
    //
    //             if (!string.IsNullOrEmpty(pnRequestModel.Name))
    //             {
    //                 customersQuery = customersQuery.Where(x => 
    //                     x.CompanyName.ToLower().Contains(pnRequestModel.Name.ToLower()) || 
    //                     x.ContactPerson.ToLower().Contains(pnRequestModel.Name.ToLower()) ||
    //                     x.Phone.Contains(pnRequestModel.Name) ||
    //                     x.VatNumber.Contains(pnRequestModel.Name) ||
    //                     x.Email.Contains(pnRequestModel.Name));
				// }
    //             else
    //             {
    //                 customersPnModel.Total = 0;
    //                 customersPnModel.Customers = new List<CustomerModel>();
    //                 return new OperationDataResult<CustomersModel>(true, customersPnModel);
    //             }
    //
				// customersQuery =
				// 	customersQuery.Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
    //                 .Skip(pnRequestModel.Offset)
    //                 .Take(pnRequestModel.PageSize);
    //
    //             List<CustomerModel> customers = await customersQuery.Select(x => new CustomerModel()
    //             {
    //                 Id = x.Id,
    //                 Description = x.Description,
    //                 Email = x.Email,
    //                 ContactPerson = x.ContactPerson,
    //                 CompanyName = x.CompanyName,
    //                 Phone = x.Phone,
    //                 CityName = x.CityName,
    //                 CompanyAddress = x.CompanyAddress,
    //                 CompanyAddress2 = x.CompanyAddress2,
    //                 CountryCode = x.CountryCode,
    //                 CreatedBy = x.CreatedBy,
    //                 CreatedDate = x.CreatedDate,
    //                 CustomerNo = x.CustomerNo,
    //                 EanCode = x.EanCode,
    //                 VatNumber = x.VatNumber,
    //                 ZipCode = x.ZipCode
    //
    //             }).ToListAsync();
    //             customersPnModel.Total = await _customerDbContext.Customers.CountAsync(x => x.WorkflowState != Constants.WorkflowStates.Removed);
    //             customersPnModel.Customers = customers;
    //             return new OperationDataResult<CustomersModel>(true, customersPnModel);
    //
    //         }
    //         catch (Exception e)
    //         {
    //             Trace.TraceError(e.Message);
    //             _logger.LogError(e.Message);
    //             return new OperationDataResult<CustomersModel>(false, 
    //                 _rentableItemsLocalizationService.GetString("ErrorObtainingCustomersInfo") + e.Message);
    //         }
    //     }
        public async Task<OperationResult> Create(ContractModel contractCreateModel)
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
                        Contract dbContract = await 
                            _dbContext.Contract.FirstOrDefaultAsync(x => x.ContractNr == contractCreateModel.ContractNr);
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

        public async Task<OperationDataResult<RentableItemCustomerModel>> ReadCustomer(int id)
        {
            try
            {
                RentableItemCustomerModel rentableItemCustomer = await _customerDbContext.Customers.Select(x => new RentableItemCustomerModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Phone = x.Phone,
                    CityName = x.CityName,
                    CustomerNo = x.CustomerNo,
                    ZipCode = x.ZipCode,
                    Email = x.Email,
                    ContactPerson = x.ContactPerson,
                    CreatedBy = x.CreatedBy,
                    CompanyAddress = x.CompanyAddress,
                    CompanyAddress2 = x.CompanyAddress2,
                    CompanyName = x.CompanyName,
                    CountryCode = x.CountryCode,
                    EanCode = x.EanCode,
                    VatNumber = x.VatNumber
                }).FirstOrDefaultAsync(x => x.Id == id);

                if (rentableItemCustomer == null)
                {
                    return new OperationDataResult<RentableItemCustomerModel>( false, "Customer not found");
                }
                
                return new OperationDataResult<RentableItemCustomerModel>(true, rentableItemCustomer);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemCustomerModel>(false, "Error obtaining customer");
            }
        }
        
        public async Task<OperationResult> Update(ContractModel updateModel)
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
                if (updateModel.DeleteIds.Count > 0)
                {
                    Contract dbContract = await 
                        _dbContext.Contract.FirstOrDefaultAsync(x => x.ContractNr == updateModel.ContractNr);

                    foreach (var rentableItemId in updateModel.DeleteIds)
                    {
                        ContractRentableItem deleteContractRentableItem =
                            await _dbContext.ContractRentableItem.FirstOrDefaultAsync(x =>
                                x.ContractId == dbContract.Id && x.RentableItemId == rentableItemId);
                        await deleteContractRentableItem.Delete(_dbContext);   
                    }
                }

                foreach (var rentableItemId in updateModel.RentableItemIds)
                {
                    Contract dbContract = await 
                        _dbContext.Contract.FirstOrDefaultAsync(x => x.ContractNr == updateModel.ContractNr);
                    ContractRentableItem contractRentableItem =
                        await _dbContext.ContractRentableItem.FirstOrDefaultAsync(x =>
                            x.ContractId == dbContract.Id && x.RentableItemId == rentableItemId);
                    ContractRentableItem checkContractRentableItem =
                        await _dbContext.ContractRentableItem.FirstOrDefaultAsync(
                            x => x.ContractId == dbContract.Id && x.RentableItemId == rentableItemId);
                    if (checkContractRentableItem != null)
                    {
                        contractRentableItem.WorkflowState = Constants.WorkflowStates.Created;
                        await contractRentableItem.Update(_dbContext);
                    }
                    else
                    {
                        ContractRentableItem createContractRentableItem = new ContractRentableItem();
                        createContractRentableItem.ContractId = dbContract.Id;
                        createContractRentableItem.RentableItemId = rentableItemId; 
                        await createContractRentableItem.Create(_dbContext);
                    }
                }

                return new OperationResult(true, _rentableItemsLocalizationService.GetString("ContractsUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, _rentableItemsLocalizationService.GetString("ErrorWhileUpdatingContract"));
            }
        }

        public async Task<OperationResult> Delete(int id)
        {
            Contract contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.Id == id);
            try
            {
                if (contract != null)
                {
                    IQueryable<ContractRentableItem> contractRentableItems =
                       _dbContext.ContractRentableItem.AsQueryable();
                    contractRentableItems = contractRentableItems.Where(x =>
                        x.ContractId == contract.Id && x.WorkflowState == Constants.WorkflowStates.Created);
                    var list = await contractRentableItems.ToListAsync();
                    foreach (var contractRentable in list)
                    {
                        await contractRentable.Delete(_dbContext);
                    }
                    await contract.Delete(_dbContext);
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, _rentableItemsLocalizationService.GetString("ErrorWhileDeletingContract"));
            }
        }
    }
}
