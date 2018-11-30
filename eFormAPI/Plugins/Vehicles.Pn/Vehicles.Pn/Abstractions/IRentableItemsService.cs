using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemsService
    {
        OperationDataResult<RentableItemsModel> GetAllRentableItems(RentableItemsRequestModel pnRequestModel);
        OperationResult CreateRentableItem(RentableItemModel rentableItemPnCreateModel);
        OperationResult UpdateRentableItem(RentableItemModel rentableItemPnUpdateModel);
        OperationResult DeleteRentableItem(RentableItemModel rentableItemPnDeleteModel);
    }
}
