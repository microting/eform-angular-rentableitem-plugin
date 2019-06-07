using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemsService
    {
        Task<OperationDataResult<RentableItemsModel>> GetAllRentableItems(RentableItemsRequestModel pnRequestModel);
        Task<OperationResult> CreateRentableItem(RentableItemModel rentableItemPnCreateModel);
        Task<OperationResult> UpdateRentableItem(RentableItemModel rentableItemPnUpdateModel);
        Task<OperationResult> DeleteRentableItem(int Id);
    }
}
