using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    public class ContractRentableItemController : Controller
    {
        private readonly IContractRentableItemService _contractRentableItemService;

        public ContractRentableItemController(IContractRentableItemService contractRentableItemService)
        {
            _contractRentableItemService = contractRentableItemService;
        }

        [HttpGet]
        [Route("api/contract-rentable-item/{contractId}")]
        public async Task<OperationDataResult<RentableItemsModel>> Index(int contractId)
        {
            return await _contractRentableItemService.Index(contractId);
        }
    }
}