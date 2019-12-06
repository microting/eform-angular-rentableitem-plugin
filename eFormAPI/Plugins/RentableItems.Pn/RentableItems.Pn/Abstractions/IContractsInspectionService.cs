using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IContractsInspectionService
    {

        Task<OperationDataResult<ContractInspectionsModel>> Index(ContractInspectionsRequestModel contractsRequestModel);
        Task<OperationResult> Create(ContractInspectionModel contractInspectionCreateModel);
        Task<OperationResult> Update(ContractInspectionModel contractInspectionUpdateModel);
        Task<OperationResult> Delete(int id);
    }
}
