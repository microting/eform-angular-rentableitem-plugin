using System;
using System.Collections.Generic;
using System.Text;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IContractsService
    {
        OperationDataResult<ContractsModel> GetAllContracts(ContractsRequestModel contractsRequestModel);
        OperationResult CreateContract(ContractModel contractCreateModel);
        OperationResult UpdateContract(ContractModel contractUpdateModel);
        OperationResult DeleteContract(ContractModel contractDeleteModel);
    }
}
