using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    public class RentableItemsController : Controller
    {
        private readonly IRentableItemsService _rentableItemsService;

        public RentableItemsController(IRentableItemsService rentableItemsService)
        {
            _rentableItemsService = rentableItemsService;
        }

        [HttpPost]
        [Route("api/rentableItems-pn")]
        public OperationDataResult<RentableItemsModel> GetAllRentableIems([FromBody] RentableItemsRequestModel requestModel)
        {
            return _rentableItemsService.GetAllRentableItems(requestModel);
        }

        [HttpPost]
        [Route("api/rentableItems-pn/create-rentableItem")]
        public OperationResult CreateRentableItem([FromBody] RentableItemModel rentableItemCreateModel)
        {
            return _rentableItemsService.CreateRentableItem(rentableItemCreateModel);
        }

        [HttpPost]
        [Route("api/rentableItems-pn/update-rentableItem")]
        public OperationResult UpdateRentableItem([FromBody] RentableItemModel rentableItemUpdateModel)
        {
            return _rentableItemsService.UpdateRentableItem(rentableItemUpdateModel);
        }

        [HttpDelete]
        [Route("api/rentableItems-pn/delete-rentableItem")]
        public OperationResult DeleteRentableItem([FromBody] RentableItemModel rentableItemDeleteModel)
        {
            return _rentableItemsService.DeleteRentableItem(rentableItemDeleteModel);
        }
    }
}