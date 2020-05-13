using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data.Consts;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    [Route("api/rentable-items-pn/rentable-items")]
    public class RentableItemsController : Controller
    {
        private readonly IRentableItemsService _rentableItemsService;

        public RentableItemsController(IRentableItemsService rentableItemsService)
        {
            _rentableItemsService = rentableItemsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<OperationDataResult<RentableItemsModel>> Index(RentableItemsRequestModel requestModel)
        {
            return await _rentableItemsService.Index(requestModel);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Policy = RentableItemsClaims.AccessRentableItemsPlugin)]
        public async Task<OperationResult> Create([FromBody] RentableItemModel rentableItemCreateModel)
        {
            return await _rentableItemsService.Create(rentableItemCreateModel);
        }

        [HttpPut]
        [Route("")]
        public async Task<OperationResult> Update([FromBody] RentableItemModel rentableItemUpdateModel)
        {
            return await _rentableItemsService.Update(rentableItemUpdateModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<OperationResult> Delete(int id)
        {
            return await _rentableItemsService.Delete(id);
        }
    }
}