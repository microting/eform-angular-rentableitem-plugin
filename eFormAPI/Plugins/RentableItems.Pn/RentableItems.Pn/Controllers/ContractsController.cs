using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly IContractsService _contractsService;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpPost]
        [Route("api/contracts")]
        public async Task<OperationDataResult<ContractsModel>> GetAllContracts([FromBody] ContractsRequestModel requestModel)
        {
            return await _contractsService.GetAllContracts(requestModel);
        }

        [HttpPost]
        [Route("api/contracts/create-contract")]
        public async Task<OperationResult> CreateContract([FromBody] ContractModel contractCreateModel)
        {
            return await _contractsService.CreateContract(contractCreateModel);
        }

        [HttpPost]
        [Route("api/contracts/update-contract")]
        public async Task<OperationResult> UpdateContract([FromBody] ContractModel contractUpdateModel)
        {
            return await _contractsService.UpdateContract(contractUpdateModel);
        }

        [HttpDelete]
        [Route("api/contracts/delete-contract")]
        public async Task<OperationResult> DeleteContract([FromBody] ContractModel contractDeleteModel)
        {
            return await _contractsService.DeleteContract(contractDeleteModel);
        }

    }
}
