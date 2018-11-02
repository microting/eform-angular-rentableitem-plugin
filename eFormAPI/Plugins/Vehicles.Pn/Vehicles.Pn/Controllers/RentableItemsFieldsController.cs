using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models.Fields;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;


namespace RentableItems.Pn.Controllers
{
    [Authorize]
    public class RentableItemsFieldsController : Controller
    {
        private readonly IRentableItemFieldsService _fieldsService;

        public RentableItemsFieldsController(IRentableItemFieldsService fieldsService)
        {
            _fieldsService = fieldsService;
        }

        [HttpGet]
        [Route("api/rentable-items/fields")]
        public OperationDataResult<RentableItemsFieldsUpdateModel> GetFields()
        {
            return _fieldsService.GetFields();
        }

        [HttpPut]
        [Route("api/rentable-items/fields")]
        public OperationResult UpdateFields([FromBody] RentableItemsFieldsUpdateModel fieldsModel)
        {
            return _fieldsService.UpdateFields(fieldsModel);
        }
    }
}
