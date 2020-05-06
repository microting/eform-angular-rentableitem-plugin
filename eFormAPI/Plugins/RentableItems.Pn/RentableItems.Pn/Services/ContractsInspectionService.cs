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
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Consts;
using RentableItems.Pn.Infrastructure.Models;
using RentableItems.Pn.Infrastructure.Models.Customer;

namespace RentableItems.Pn.Services
{
    public class ContractsInspectionService : IContractsInspectionService
    {
        private readonly ILogger<ContractsInspectionService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly eFormRentableItemPnDbContext _dbContext;
        private readonly CustomersPnDbAnySql _customersPnDbContext;
        private readonly IEFormCoreService _coreHelper;
        private readonly CustomersPnDbAnySql _customerDbContext;

        public ContractsInspectionService(eFormRentableItemPnDbContext dbContext,
            CustomersPnDbAnySql customerDbContext,
            CustomersPnDbAnySql customersPnDbContext,
            ILogger<ContractsInspectionService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService
            )
        {
            _dbContext = dbContext;
            _customerDbContext = customerDbContext;
            _customersPnDbContext = customersPnDbContext;
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
                if (!string.IsNullOrEmpty(contractInspectionsPnRequestModel.SortColumnName)
                    && contractInspectionsPnRequestModel.SortColumnName != "")
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
                List<ContractInspection> contractInspections = await contractInspectionsQuery.ToListAsync();
                contractInspections.ForEach(contractInspection =>
                {
                    ContractInspectionItem contractInspectionItem =
                        _dbContext.ContractInspectionItem.FirstOrDefault(x =>
                            x.ContractInspectionId == contractInspection.Id);

                    Contract contract = _dbContext.Contract.Single(x => x.Id == contractInspection.ContractId);

                    Customer customer =
                        _customerDbContext.Customers.Single(x => x.Id == contract.CustomerId);
                    CustomerModel customerModel = new CustomerModel()
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

                    contractInspectionsModel.ContractInspections.Add(new ContractInspectionModel()
                    {
                        SdkCaseId = contractInspectionItem.SDKCaseId,
                        eFormId = rentableItemModels.First().EFormId,
                        ContractId = contractInspection.ContractId,
                        ContractStart = contract.ContractStart,
                        ContractEnd = contract.ContractEnd,
                        DoneAt = contractInspection.DoneAt,
                        Id = contractInspection.Id,
                        Status = contractInspectionItem.Status,
                        Customer = customerModel,
                        RentableItems = rentableItemModels
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
                    
                    Contract dbContract =
                        await _dbContext.Contract.FirstOrDefaultAsync(x =>
                            x.Id == contractInspectionCreateModel.ContractId);
                    Customer dbCustomer =
                        await _customersPnDbContext.Customers.SingleOrDefaultAsync(x => x.Id == dbContract.CustomerId);

                    MainElement mainElement = await _core.TemplateRead(eFormId);
                    mainElement.Repeated = 1;
                    mainElement.EndDate = DateTime.Now.AddDays(14).ToUniversalTime();
                    mainElement.StartDate = DateTime.Now.ToUniversalTime();
                    CDataValue cDataValue = new CDataValue();
                    mainElement.Label = "";
                    mainElement.Label += string.IsNullOrEmpty(rentableItem.SerialNumber)
                        ? ""
                        : $"{rentableItem.SerialNumber}";
                    mainElement.Label += string.IsNullOrEmpty(rentableItem.VinNumber)
                        ? ""
                        : $"{rentableItem.VinNumber}";
                    mainElement.Label += string.IsNullOrEmpty(rentableItem.Brand)
                        ? ""
                        : $"<br>{rentableItem.Brand}";
                    mainElement.Label += string.IsNullOrEmpty(rentableItem.ModelName)
                        ? ""
                        : $"<br>{rentableItem.ModelName}";
                    mainElement.Label += string.IsNullOrEmpty(dbCustomer.ContactPerson)
                        ? ""
                        : $"<br>{dbCustomer.ContactPerson}";

                    cDataValue = new CDataValue();
                    cDataValue.InderValue = $"<b>Kontrakt Nr:<b>{dbContract.ContractNr.ToString()}<br>";
                    cDataValue.InderValue += $"<b>Kunde Nr:<b>{dbContract.CustomerId.ToString()}";
                    
                    List<SiteDto> sites = new List<SiteDto>();

                    LogEvent($"found siteId {contractInspectionCreateModel.SiteId}");
                    sites.Add(await _core.SiteRead(contractInspectionCreateModel.SiteId));

                    foreach (SiteDto siteDto in sites)
                    {

                        int? sdkCaseId = await _core.CaseCreate(mainElement, "", siteDto.SiteId);

                        if (sdkCaseId != null)
                        {
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
                                SDKCaseId = (int) sdkCaseId,
                                Status = 33
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
