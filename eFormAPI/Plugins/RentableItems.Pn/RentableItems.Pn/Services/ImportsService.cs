using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eFormCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using Newtonsoft.Json.Linq;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Helpers;
using RentableItems.Pn.Infrastructure.Models;
using RentableItems.Pn.Infrastructure.Models.Customer;

namespace RentableItems.Pn.Services
{
    public class ImportsService : IImportsService
    {
        private readonly IEFormCoreService _coreHelper;
        private readonly CustomersPnDbAnySql _customerDbContext;
        private readonly eFormRentableItemPnDbContext _dbContext;
        private readonly ILogger<ContractRentableItemService> _logger;

        public ImportsService(IEFormCoreService coreHelper, 
            CustomersPnDbAnySql customerDbContext,
            ILogger<ContractRentableItemService> logger,
            eFormRentableItemPnDbContext dbContext)
        {
            _coreHelper = coreHelper;
            _customerDbContext = customerDbContext;
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<OperationResult> Import(ImportModel importModel)
        {
            try
            {
                JToken rawJson = JToken.Parse(importModel.ImportList);
                int eFormId = importModel.eFormId;
                JToken rawHeadersJson = JToken.Parse(importModel.Headers);

                JToken headers = rawHeadersJson;
                IEnumerable<JToken> rentableObjects = rawJson.Skip(2);
                        
                Core core = await _coreHelper.GetCore();

                foreach (JToken rentableObject in rentableObjects)
                {
                    Customer customer = null;
                    bool companyNameExists = int.TryParse(headers[1]["headerValue"].ToString(), out int companyNameColumn);
                    
                    if (companyNameExists)
                    {
                        customer = await FindCustomer(companyNameExists, companyNameColumn, headers, rentableObject);

                        if (customer == null)
                        {
                            CustomerFullModel customerModel =
                                CustomerHelper.ComposeValues(new CustomerFullModel(), headers, rentableObject);

                            customer = new Customer
                            {
                                CompanyName = customerModel.CompanyName
                            };
                            await customer.Create(_customerDbContext);
                        }
                    }

                    RentableItem rentableItem = null;
                    bool brandExists = int.TryParse(headers[2]["headerValue"].ToString(), out int brandColumn);
                    bool modelExists = int.TryParse(headers[3]["headerValue"].ToString(), out int modelColumn);
                    bool registrationDateExists =
                        int.TryParse(headers[4]["headerValue"].ToString(), out int registrationDateColumn);
                    bool vinNumberExists = int.TryParse(headers[5]["headerValue"].ToString(), out int vinNumberColumn);

                    if (brandExists
                        && modelExists
                        && registrationDateExists
                        && vinNumberExists)
                    {
                         rentableItem = await FindRentableItem(brandExists, brandColumn, modelExists, modelColumn,
                            registrationDateExists, registrationDateColumn, vinNumberExists, vinNumberColumn, headers,
                            rentableObject);

                        if (rentableItem == null)
                        {
                            RentableItemModel rentableItemModel =
                                RentableItemHelper.ComposeValues(new RentableItemModel(), headers, rentableObject);

                            rentableItem = new RentableItem
                            {
                                Brand = rentableItemModel.Brand,
                                ModelName = rentableItemModel.ModelName,
                                RegistrationDate = rentableItemModel.RegistrationDate,
                                VinNumber = rentableItemModel.VinNumber,
                                eFormId = eFormId
                            };
                            await rentableItem.Create(_dbContext);    
                        }
                    }

                    Contract contract = null;
                    bool contractNumberExists =
                        int.TryParse(headers[0]["headerValue"].ToString(), out int contractNumberColumn);
                    bool contractStartExists =
                        int.TryParse(headers[6]["headerValue"].ToString(), out int contractStartColumn);
                    bool contractEndExists = int.TryParse(headers[7]["headerValue"].ToString(), out int contractEndColumn);
                    int customerID = customer.Id;
                    if (contractNumberExists 
                        && contractStartExists
                        && contractEndExists
                        && customerID != null)
                    {
                        contract = await FindContract(contractNumberExists, contractNumberColumn, contractStartExists,
                            contractStartColumn, contractEndExists, contractEndColumn, customerID, headers, rentableObject);
                        if (contract == null)
                        {
                            ContractModel contractModel =
                                ContractsHelper.ComposeValues(new ContractModel(), headers, rentableObject);

                            contract = new Contract
                            {
                                CustomerId = customerID,
                                ContractEnd = contractModel.ContractEnd,
                                ContractStart = contractModel.ContractStart,
                                ContractNr = contractModel.ContractNr
                            };
                            await contract.Create(_dbContext);
                        }
                    }

                    ContractRentableItem contractRentableItem = null;
                    if (contract.Id != null && rentableItem.Id != null)
                    {
                        contractRentableItem = await FindtContractRentableItem(contract.Id, rentableItem.Id);

                        if (contractRentableItem == null)
                        {
                            contractRentableItem = new ContractRentableItem
                            {
                                ContractId = contract.Id,
                                RentableItemId = rentableItem.Id
                            };
                            await contractRentableItem.Create(_dbContext);
                        }
                    }
                }
                return new OperationResult(true, "Import Successful");
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false, "You goofed up");
            }
            
        }
        
        private async Task<Customer> FindCustomer(bool companyNameExists, int companyNameColumn, JToken headers, JToken customerObj)
        {
            Customer customer = null;
            
            if (companyNameExists)
            {
                string companyName = customerObj[companyNameColumn].ToString();
                customer = await _customerDbContext.Customers.SingleOrDefaultAsync(x => x.CompanyName == companyName && x.WorkflowState == Constants.WorkflowStates.Created);
            }
    
            return customer;
        }

        private async Task<RentableItem> FindRentableItem(bool brandExists, int brandColumn, bool modelExists, int modelColumn,
            bool registrationDateExists,int registrationDateColumn, bool vinNumberExists, int vinNumberColumn,
            JToken headers, JToken rentableObj)
        {
            RentableItem rentableItem = null;
            if (brandExists)
            {
                string brand = rentableObj[brandColumn].ToString();
                rentableItem = await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.Brand == brand && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            if (modelExists)
            {
                string model = rentableObj[modelColumn].ToString();
                rentableItem = await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.ModelName == model && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            if (registrationDateExists)
            {
                DateTime registrationDate = (DateTime)rentableObj[registrationDateColumn];
                rentableItem =
                    await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.RegistrationDate == registrationDate && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            if (vinNumberExists)
            {
                string vinNumber = rentableObj[vinNumberColumn].ToString();
                rentableItem = await _dbContext.RentableItem.SingleOrDefaultAsync(x => x.VinNumber == vinNumber && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            return rentableItem;
        }

        private async Task<Contract> FindContract(bool contractNumberExists, int contractNumberColumn,
            bool contractStartExists, int contractStartColumn, bool contractEndExists, int contractEndColumn, int? customerId,
            JToken headers, JToken rentableObj)
        {
            Contract contract = null;

            if (contractNumberExists)
            {
                int? contractNr = (int?) rentableObj[contractNumberColumn];
                contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.ContractNr == contractNr && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            if (contractStartExists)
            {
                DateTime contractStart = (DateTime) rentableObj[contractStartColumn];
                contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.ContractStart == contractStart && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            if (contractEndExists)
            {
                DateTime contractEnd = (DateTime) rentableObj[contractEndColumn];
                contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.ContractEnd == contractEnd && x.WorkflowState == Constants.WorkflowStates.Created);
            }
            if (customerId != null)
            {
                contract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.CustomerId == customerId && x.WorkflowState == Constants.WorkflowStates.Created);
            }

            return contract;
        }

        private async Task<ContractRentableItem> FindtContractRentableItem(int? contractId, int? rentableId)
        {
            ContractRentableItem contractRentableItem = null;

            if (contractId != null)
            {
                contractRentableItem =
                    await _dbContext.ContractRentableItem.SingleOrDefaultAsync(x => x.ContractId == contractId);
            }

            if (rentableId != null)
            {
                contractRentableItem =
                    await _dbContext.ContractRentableItem.SingleOrDefaultAsync(x => x.RentableItemId == rentableId);
            }

            return contractRentableItem;
        }
    }
}