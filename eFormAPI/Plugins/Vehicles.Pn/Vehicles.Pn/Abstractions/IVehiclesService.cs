using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Vehicles.Pn.Infrastructure.Models;

namespace Vehicles.Pn.Abstractions
{
    public interface IVehiclesService
    {
        OperationDataResult<VehiclesModel> GetAllVehicles(VehiclesRequestModel pnRequestModel);
        OperationResult CreateVehicle(VehicleModel vehiclePnCreateModel);
        OperationResult UpdateVehicle(VehicleModel vehiclePnUpdateModel);
    }
}
