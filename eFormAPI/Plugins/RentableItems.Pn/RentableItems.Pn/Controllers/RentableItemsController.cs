using System.Threading.Tasks;
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
        public async Task<OperationDataResult<RentableItemsModel>> GetAllRentableIems([FromBody] RentableItemsRequestModel requestModel)
        {
            return await _rentableItemsService.GetAllRentableItems(requestModel);
        }

        [HttpPost]
        [Route("api/rentableItems-pn/create-rentableItem")]
        public async Task<OperationResult> CreateRentableItem([FromBody] RentableItemModel rentableItemCreateModel)
        {
            return await _rentableItemsService.CreateRentableItem(rentableItemCreateModel);
        }

        [HttpPost]
        [Route("api/rentableItems-pn/update-rentableItem")]
        public async Task<OperationResult> UpdateRentableItem([FromBody] RentableItemModel rentableItemUpdateModel)
        {
            return await _rentableItemsService.UpdateRentableItem(rentableItemUpdateModel);
        }

        [HttpDelete]
        [Route("api/rentableItems-pn/delete-rentableItem/{id}")]
        public  async Task<OperationResult> DeleteRentableItem(int id)
        {
            return await _rentableItemsService.DeleteRentableItem(id);
        }
    }
}