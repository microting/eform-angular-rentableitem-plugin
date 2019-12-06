using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;
using RentableItems.Pn.Infrastructure.Models.Customer;

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
        public async Task<OperationDataResult<ContractsModel>> Index([FromBody] ContractsRequestModel requestModel)
        {
            return await _contractsService.Index(requestModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/contracts/customers")]
        public async Task<OperationDataResult<CustomersModel>> IndexCustomers(
            [FromBody] CustomersRequestModel requestModel)
        {
            return await _contractsService.IndexCustomers(requestModel);
        }

        [HttpPost]
        [Route("api/contracts/create-contract")]
        public async Task<OperationResult> Create([FromBody] ContractModel contractCreateModel)
        {
            return await _contractsService.Create(contractCreateModel);
        }
        
        [HttpGet]
        [Route("api/contracts/customers/{id}")]
        public async Task<OperationDataResult<CustomerModel>> ReadCustomer(int id)
        {
            return await _contractsService.ReadCustomer(id);
        }
        
        [HttpPost]
        [Route("api/contracts/update-contract")]
        public async Task<OperationResult> Update([FromBody] ContractModel contractUpdateModel)
        {
            return await _contractsService.Update(contractUpdateModel);
        }

        [HttpDelete]
        [Route("api/contracts/delete-contract/{id}")]
        public async Task<OperationResult> Delete(int id)
        {
            return await _contractsService.Delete(id);
        }

        
    }
}
