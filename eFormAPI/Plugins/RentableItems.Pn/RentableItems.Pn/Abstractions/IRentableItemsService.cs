using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemsService
    {
        Task<OperationDataResult<RentableItemsModel>> Index(RentableItemsRequestModel pnRequestModel);
        Task<OperationResult> Create(RentableItemModel rentableItemPnCreateModel);
        Task<OperationResult> Update(RentableItemModel rentableItemPnUpdateModel);
        Task<OperationResult> Delete(int Id);
    }
}
