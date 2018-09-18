using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Vehicles.Pn.Abstractions;
using Vehicles.Pn.Infrastructure.Models;

namespace Vehicles.Pn.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        [HttpPost]
        [Route("api/vehicles-pn")]
        public OperationDataResult<VehiclesModel> GetAllVehicles(VehiclesRequestModel requestModel)
        {
            return _vehiclesService.GetAllVehicles(requestModel);
        }

        [HttpPost]
        [Route("api/vehicles-pn/create-vehicle")]
        public OperationResult CreateVehicle(VehicleModel vehicleCreateModel)
        { 
            return _vehiclesService.CreateVehicle(vehicleCreateModel);
        }

        [HttpPost]
        [Route("api/vehicles-pn/update-vehicle")]
        public OperationResult UpdateVehicle(VehicleModel vehicleUpdateModel)
        {
            return _vehiclesService.UpdateVehicle(vehicleUpdateModel);
        }
    }
}