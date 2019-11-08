using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;
using RentableItems.Pn.Infrastructure.Models.Customer;

namespace RentableItems.Pn.Abstractions
{
    public interface IContractsService
    {
        Task<OperationDataResult<ContractsModel>> GetAllContracts(ContractsRequestModel contractsRequestModel);
        Task<OperationResult> CreateContract(ContractModel contractCreateModel);
        Task<OperationResult> UpdateContract(ContractModel contractUpdateModel);
        Task<OperationResult> DeleteContract(int id);
        Task<OperationDataResult<CustomersModel>> GetAllCustomers(CustomersRequestModel customersRequestModel);
        Task<OperationDataResult<CustomerModel>> GetSingleCustomer(int id);

    }
}
