using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemsSettingsService
    {
        Task<OperationDataResult<RentableItemsSettingsModel>> GetSettings();
        Task<OperationResult> UpdateSettings(RentableItemsSettingsModel rentableItemsSettingsModel);
    }
}
