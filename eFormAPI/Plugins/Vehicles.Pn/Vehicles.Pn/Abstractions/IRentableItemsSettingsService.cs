using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemsSettingsService
    {
        OperationDataResult<RentableItemsSettingsModel> GetSettings();
        OperationResult UpdateSettings(RentableItemsSettingsModel rentableItemsSettingsModel);
    }
}
