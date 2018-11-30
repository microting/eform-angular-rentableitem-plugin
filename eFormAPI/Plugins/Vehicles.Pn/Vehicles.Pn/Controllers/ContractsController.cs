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
        public OperationDataResult<ContractsModel> GetAllContracts([FromBody] ContractsRequestModel requestModel)
        {
            return _contractsService.GetAllContracts(requestModel);
        }

        [HttpPost]
        [Route("api/contracts/create-contract")]
        public OperationResult CreateContract([FromBody] ContractModel contractCreateModel)
        {
            return _contractsService.CreateContract(contractCreateModel);
        }

        [HttpPut]
        [Route("api/contracts/update-contract")]
        public OperationResult UpdateContract([FromBody] ContractModel contractUpdateModel)
        {
            return _contractsService.UpdateContract(contractUpdateModel);
        }

        [HttpDelete]
        [Route("api/contracts/delete-contract")]
        public OperationResult DeleteContract([FromBody] ContractModel contractDeleteModel)
        {
            return _contractsService.DeleteContract(contractDeleteModel);
        }

    }
}
