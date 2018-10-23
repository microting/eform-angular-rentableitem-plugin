using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace Vehicles.Pn.Controllers
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
        [Route("api/rentbaleItems-pn/create-rentableItem")]
        public OperationResult CreateRentableItem([FromBody] RentableItemModel rentableItemCreateModel)
        {
            return _rentableItemsService.CreateRentableItem(rentableItemCreateModel);
        }

        [HttpPost]
        [Route("api/rentbaleItems-pn/update-rentableItem")]
        public OperationResult UpdateRentableItem([FromBody] RentableItemModel rentableItemUpdateModel)
        {
            return _rentableItemsService.UpdateRentableItem(rentableItemUpdateModel);
        }
    }
}