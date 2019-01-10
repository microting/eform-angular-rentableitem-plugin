using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models.Fields;
namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemFieldsService
    {
        OperationDataResult<RentableItemsFieldsUpdateModel> GetFields();
        OperationResult UpdateFields(RentableItemsFieldsUpdateModel rentableItemsFieldsModel);
    }
}
