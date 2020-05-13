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
        Task<OperationDataResult<ContractsModel>> Index(ContractsRequestModel contractsRequestModel);
        Task<OperationResult> Create(ContractModel contractCreateModel);
        Task<OperationResult> Update(ContractModel contractUpdateModel);
        Task<OperationResult> Delete(int id);

    }
}
