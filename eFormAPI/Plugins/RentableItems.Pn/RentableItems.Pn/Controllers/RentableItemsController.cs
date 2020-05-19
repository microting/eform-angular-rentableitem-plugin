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
        public Task<OperationDataResult<RentableItemsModel>> Index(RentableItemsRequestModel requestModel)
        {
            return _rentableItemsService.Index(requestModel);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Policy = RentableItemsClaims.AccessRentableItemsPlugin)]
        public Task<OperationResult> Create([FromBody] RentableItemModel rentableItemCreateModel)
        {
            return _rentableItemsService.Create(rentableItemCreateModel);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = RentableItemsClaims.AccessRentableItemsPlugin)]
        public Task<OperationDataResult<RentableItemModel>> Read(int id)
        {
            return _rentableItemsService.Read(id);
        }

        [HttpPut]
        [Route("")]
        public Task<OperationResult> Update([FromBody] RentableItemModel rentableItemUpdateModel)
        {
            return _rentableItemsService.Update(rentableItemUpdateModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<OperationResult> Delete(int id)
        {
            return _rentableItemsService.Delete(id);
        }
    }
}