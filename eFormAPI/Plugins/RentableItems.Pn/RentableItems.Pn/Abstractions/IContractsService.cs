using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

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
