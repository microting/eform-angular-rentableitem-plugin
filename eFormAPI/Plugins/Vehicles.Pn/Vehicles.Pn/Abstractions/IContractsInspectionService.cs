using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IContractsInspectionService
    {

        OperationDataResult<ContractInspectionsModel> GetAllContractInspections(ContractInspectionsRequestModel contractsRequestModel);
        OperationResult CreateContractInspection(ContractInspectionModel contractInspectionCreateModel);
        OperationResult UpdateContractInspection(ContractInspectionModel contractInspectionUpdateModel);
        OperationResult DeleteContractInspection(ContractInspectionModel contractInspectionDeleteModel);
    }
}
