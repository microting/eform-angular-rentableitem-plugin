using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IContractsInspectionService
    {

        Task<OperationDataResult<ContractInspectionsModel>> GetAllContractInspections(ContractInspectionsRequestModel contractsRequestModel);
        Task<OperationResult> CreateContractInspection(ContractInspectionModel contractInspectionCreateModel);
        Task<OperationResult> UpdateContractInspection(ContractInspectionModel contractInspectionUpdateModel);
        Task<OperationResult> DeleteContractInspection(int id);
    }
}
