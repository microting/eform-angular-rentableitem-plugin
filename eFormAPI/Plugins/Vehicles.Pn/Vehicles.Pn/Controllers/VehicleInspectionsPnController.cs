using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using eFormApi.BasePn.Infrastructure.Models.API;
using NLog;
using Vehicles.Pn.Helpers;
using Vehicles.Pn.Infrastructure.Data;
using Vehicles.Pn.Infrastructure.Data.Entities;
using Vehicles.Pn.Infrastructure.Extensions;
using Vehicles.Pn.Infrastructure.Models;

namespace Vehicles.Pn.Controllers
{
    public class VehicleInspectionsPnController : ApiController
    {
        private readonly Logger _logger;
        private readonly VehiclesPnDbContext _dbContext;

        public VehicleInspectionsPnController()
        {
            _dbContext = VehiclesPnDbContext.Create();
            _logger = LogManager.GetCurrentClassLogger();
        }


        [HttpPost]
        [Route("api/vehicleInspections-pn")]
        public OperationDataResult<VehicleInspectionsPnModel> GetAllVehicleInspection(VehiclesPnRequestModel pnRequestModel)
        {
            try
            {
                var vehicleInspectionsPnModel = new VehicleInspectionsPnModel();
                var vehicleInspectionsQuery = _dbContext.VehicleInspections.AsQueryable();
                if (!string.IsNullOrEmpty(pnRequestModel.SortColumnName))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        vehicleInspectionsQuery = vehicleInspectionsQuery.OrderByDescending(pnRequestModel.SortColumnName);
                    }
                    else
                    {
                        vehicleInspectionsQuery = vehicleInspectionsQuery.OrderBy(pnRequestModel.SortColumnName);
                    }
                }
                vehicleInspectionsPnModel.Total = vehicleInspectionsQuery.Count();
                vehicleInspectionsQuery = vehicleInspectionsQuery.Skip(pnRequestModel.Offset).Take(pnRequestModel.PageSize);
                var vehicleInspections = vehicleInspectionsQuery.ToList();
                vehicleInspections.ForEach(inspection =>
                {
                    vehicleInspectionsPnModel.Inspections.Add(new VehicleInspectionPnModel()
                    {
                        Id = inspection.Id,
                        WorkflowState = inspection.workflow_state,
                        Version = inspection.version,
                        Status = inspection.Status,
                        CreatedAt = inspection.created_at,
                        UpdatedAt = inspection.updated_at,
                        DoneAt = inspection.DoneAt,
                        VehicleContractId = inspection.VehicleContractId,
                    });
                });
                return new OperationDataResult<VehicleInspectionsPnModel>(true, vehicleInspectionsPnModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<VehicleInspectionsPnModel>(false,
                    VehiclePnLocaleHelper.GetString("ErrorObtainingVehiclesInfo"));
            }
        }

        [HttpPost]
        [Route("api/vehicleInspections-pn/create-vehicleInspection")]
        public OperationResult CreateVehicleInspection(VehicleInspectionPnModel vehicleInspectionPnCreateModel)
        {
            try
            {
                var vehicleInspectionPn = new VehicleInspection
                {
                    Id = vehicleInspectionPnCreateModel.Id,
                    workflow_state = vehicleInspectionPnCreateModel.WorkflowState,
                    version = vehicleInspectionPnCreateModel.Version,
                    Status = vehicleInspectionPnCreateModel.Status,
                    created_at = vehicleInspectionPnCreateModel.CreatedAt,
                    updated_at = vehicleInspectionPnCreateModel.UpdatedAt,
                    DoneAt = vehicleInspectionPnCreateModel.DoneAt,
                    VehicleContractId = vehicleInspectionPnCreateModel.VehicleContractId,
                };
                _dbContext.VehicleInspections.Add(vehicleInspectionPn);
                _dbContext.SaveChanges();
                return new OperationResult(true,
                    VehiclePnLocaleHelper.GetString("VehicleInspectionCreated"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationResult(false, VehiclePnLocaleHelper.GetString("ErrorWhileCreatingVehicleÍnspection"));
            }
        }

        [HttpPost]
        [Route("api/vehicleInspections-pn/update-vehicleInspection")]
        public OperationResult UpdateVehicleInspection(VehicleInspectionPnModel vehicleInspectionPnUpdateModel)
        {
            try
            {
                var vehicleInspection = _dbContext.VehicleInspections.FirstOrDefault(x => x.Id == vehicleInspectionPnUpdateModel.Id);
                if (vehicleInspection == null)
                {
                    return new OperationResult(false, "Vehicle not found");
                }
                vehicleInspection.Id = vehicleInspectionPnUpdateModel.Id;
                vehicleInspection.workflow_state = vehicleInspectionPnUpdateModel.WorkflowState;
                vehicleInspection.version = vehicleInspectionPnUpdateModel.Version;
                vehicleInspection.Status = vehicleInspectionPnUpdateModel.Status;
                vehicleInspection.created_at = vehicleInspectionPnUpdateModel.CreatedAt;
                vehicleInspection.updated_at = vehicleInspectionPnUpdateModel.UpdatedAt;
                vehicleInspection.DoneAt = vehicleInspectionPnUpdateModel.DoneAt;
                vehicleInspection.VehicleContractId = vehicleInspectionPnUpdateModel.VehicleContractId;
                _dbContext.SaveChanges();
                return new OperationDataResult<VehicleInspectionPnModel>(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<VehicleInspectionPnModel>(false, VehiclePnLocaleHelper.GetString("ErrorWhileUpdatingVehicleInfo"));
            }
        }
    }
}

