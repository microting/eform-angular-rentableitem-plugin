using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    [Route("api/rentable-items-pn/contracts")]
    public class ContractsController : Controller
    {
        private readonly IContractsService _contractsService;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<OperationDataResult<ContractsModel>> Index(ContractsRequestModel requestModel)
        {
            return await _contractsService.Index(requestModel);
        }

        [HttpPost]
        [Route("")]
        public async Task<OperationResult> Create([FromBody] ContractModel contractCreateModel)
        {
            return await _contractsService.Create(contractCreateModel);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<OperationResult> Update([FromBody] ContractModel contractUpdateModel)
        {
            return await _contractsService.Update(contractUpdateModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<OperationResult> Delete(int id)
        {
            return await _contractsService.Delete(id);
        }

        
    }
}
