using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    [Route("api/rentable-items-pn/contract-rentable-items")]
    public class ContractRentableItemController : Controller
    {
        private readonly IContractRentableItemService _contractRentableItemService;

        public ContractRentableItemController(IContractRentableItemService contractRentableItemService)
        {
            _contractRentableItemService = contractRentableItemService;
        }

        [HttpGet]
        [Route("{contractId}")]
        public async Task<OperationDataResult<RentableItemsModel>> Index(int contractId)
        {
            return await _contractRentableItemService.Index(contractId);
        }
    }
}