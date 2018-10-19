using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using eFormApi.BasePn.Infrastructure.Models.API;
using Newtonsoft.Json.Linq;
using NLog;
using Vehicles.Pn.Helpers;
using Vehicles.Pn.Infrastructure.Data;
using Vehicles.Pn.Infrastructure.Data.Entities;
using Vehicles.Pn.Infrastructure.Extensions;
using Vehicles.Pn.Infrastructure.Models;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Data;
namespace Vehicles.Pn.Controllers
{
    public class VehiclesPnController : ApiController
    {
        private readonly Logger _logger;
        private readonly VehiclesPnDbContext _dbContext;
        private readonly CustomersPnDbContext _customersDbContext;

        public VehiclesPnController()
        {
            _dbContext = VehiclesPnDbContext.Create();
            _customersDbContext = CustomersPnDbContext.Create();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("api/vehicles-pn")]
        public OperationDataResult<VehiclesPnModel> GetAllVehicles(VehiclesPnRequestModel pnRequestModel)
        {
            try
            {
                var vehiclesPnModel = new VehiclesPnModel();
                var vehiclesQuery = _dbContext.Vehicles.AsQueryable();
                if (!string.IsNullOrEmpty(pnRequestModel.SortColumnName))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        vehiclesQuery = vehiclesQuery.OrderByDescending(pnRequestModel.SortColumnName);
                    }
                    else
                    {
                        vehiclesQuery = vehiclesQuery.OrderBy(pnRequestModel.SortColumnName);
                    }
                }
                vehiclesPnModel.Total = vehiclesQuery.Count();
                vehiclesQuery = vehiclesQuery.Skip(pnRequestModel.Offset).Take(pnRequestModel.PageSize);
                var vehicles = vehiclesQuery.ToList();
                vehicles.ForEach(vehicle =>
                {
                    vehiclesPnModel.Vehicles.Add(new VehiclePnModel()
                    {
                        VinNumber = vehicle.VinNumber,
                        //ContractEndDate = vehicle.ContractEndDate, // Contract Model
                        //ContractStartDate = vehicle.ContractStartDate, // Contract Model
                        //CustomerName = vehicle.CustomerName, // Custoer Model
                        RegistrationDate = vehicle.RegistrationDate,
                        Brand = vehicle.Brand,
                        //ContactNumber = vehicle.ContractNumber, // Contract Model
                        ModelName = vehicle.ModelName,
                        Id = vehicle.Id,
                    });
                });
                return new OperationDataResult<VehiclesPnModel>(true, vehiclesPnModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<VehiclesPnModel>(false,
                    VehiclePnLocaleHelper.GetString("ErrorObtainingVehiclesInfo"));
            }
        }

        [HttpPost]
        [Route("api/vehicles-pn/create-vehicle")]
        public OperationResult CreateVehicle(VehiclePnModel vehiclePnCreateModel)
        {
            try
            {
                var vehiclePn = new Vehicle
                {
                    VinNumber = vehiclePnCreateModel.VinNumber,
                    Brand = vehiclePnCreateModel.Brand,
                    //ContractNumber = vehiclePnCreateModel.ContactNumber, // Contract Model
                    //ContractEndDate = vehiclePnCreateModel.ContractEndDate, // Contract Model
                    //ContractStartDate = vehiclePnCreateModel.ContractStartDate, // Contract Model
                    //CustomerName = vehiclePnCreateModel.CustomerName, // Customer Model
                    ModelName = vehiclePnCreateModel.ModelName,
                    RegistrationDate = vehiclePnCreateModel.RegistrationDate,
                    PlateNumber = vehiclePnCreateModel.PlateNumber,
                };
                _dbContext.Vehicles.Add(vehiclePn);
                _dbContext.SaveChanges();
                return new OperationResult(true,
                    VehiclePnLocaleHelper.GetString("VehicleCreated", vehiclePnCreateModel.Brand,
                        vehiclePnCreateModel.ModelName));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationResult(false, VehiclePnLocaleHelper.GetString("ErrorWhileCreatingVehicle"));
            }
        }

        [HttpPost]
        [Route("api/vehicles-pn/create-contract")]
        public OperationResult CreateContract(VehicleContractModel vehicleContractCreateModel)
        {
            try
            {

                Vehicle existingVehicle = _dbContext.Vehicles.SingleOrDefault(x => x.id == vehicleContractCreateModel.VehicleId);
                if (existingVehicle == null)
                {
                    Vehicle vehicle = new Vehicle();
                    vehicleContractCreateModel.VehicleId = vehicle.id;

                    _dbContext.Vehicles.Add(vehicle);
                    _dbContext.SaveChanges();
                }

                Customer existingCustomer = _customersDbContext.Customers.SingleOrDefault(x => x.Id == vehicleContractCreateModel.CustomerId);
                if (existingCustomer == null)
                {
                    Customer customer = new Customer();
                    vehicleContractCreateModel.CustomerId = customer.Id;

                    _customersDbContext.Customers.Add(customer);
                    _customersDbContext.SaveChanges();
                }

                VehicleContract existingVehicleContract = _dbContext.VehicleContracts.SingleOrDefault(x => x.ContractNumber == vehicleContractCreateModel.ContractNumber);
                if (existingVehicleContract == null)
                {
                    VehicleContract vehicleContract = new VehicleContract();
                    vehicleContract.ContractEndDate = vehicleContractCreateModel.ContractEndDate;
                    vehicleContract.ContractNumber = vehicleContractCreateModel.ContractNumber;
                    vehicleContract.ContractStartDate = vehicleContractCreateModel.ContractStartDate;
                    vehicleContract.VehicleId = existingVehicle.Id;
                    vehicleContract.CustomerId = existingCustomer.Id;

                    _dbContext.VehicleContracts.Add(vehicleContract);
                    _dbContext.SaveChanges();
                }

                return new OperationResult(true);
            }

            catch(Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationResult(false);
            }
           }

        [HttpPost]
        [Route("api/vehicles-pn/update-vehicle")]
        public OperationResult UpdateVehicle(VehiclePnModel vehiclePnUpdateModel)
        {
            try
            {
                //int id = 4;
                //var vehicleContract = _dbContext.VehicleContracts.FirstOrDefault(x => x.Id == id);
                var vehicle = _dbContext.Vehicles.FirstOrDefault(x => x.Id == vehiclePnUpdateModel.Id);
                if (vehicle == null)
                {
                    return new OperationResult(false, "Vehicle not found");
                }
                vehicle.VinNumber = vehiclePnUpdateModel.VinNumber;
                vehicle.Brand = vehiclePnUpdateModel.Brand;
                //vehicle.ContractNumber = vehiclePnUpdateModel.ContactNumber; // Contract Model
                //vehicle.ContractEndDate = vehiclePnUpdateModel.ContractEndDate; // Contract Model
                //vehicle.ContractStartDate = vehiclePnUpdateModel.ContractStartDate; // Contract Model
                //vehicle.CustomerName = vehiclePnUpdateModel.CustomerName; // Customer Model
                vehicle.ModelName = vehiclePnUpdateModel.ModelName;
                vehicle.RegistrationDate = vehiclePnUpdateModel.RegistrationDate;
                _dbContext.SaveChanges();
                return new OperationDataResult<VehiclesPnModel>(true);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<VehiclesPnModel>(false, VehiclePnLocaleHelper.GetString("ErrorWhileUpdatingVehicleInfo"));
            }
        }

        [HttpPost]
        [Route("api/vehicles-pn/import-vehicles")]
        public OperationResult ImportVehicles(VehicleImportModel vehiclesAsJson)
        {
            {
                JToken rawJson = JRaw.Parse(vehiclesAsJson.ImportList);
                JToken rawHeadersJson = JRaw.Parse(vehiclesAsJson.Headers);

                var headers = rawHeadersJson;
                var vehicleObjects = rawJson.Skip(1);

                foreach (var vehicleObj in vehicleObjects)
                {

                    string name = "Jens Jensen";
                    string address = "Vejen 2";
                    string ZipCode = "5000";
                    string City = "Odense C";

                    Customer existingCustomer = _customersDbContext.Customers.SingleOrDefault(x => x.CompanyName == name && x.CompanyAddress == address && x.ZipCode == ZipCode && x.CityName == City);
                    if (existingCustomer == null)
                    {
                        Customer customer = new Customer();
                        customer.CompanyName = vehicleObj[int.Parse(headers[1]["headerValue"].ToString())].ToString();
                        customer.CompanyAddress = address;
                        customer.ZipCode = ZipCode;
                        customer.CityName = City;
                        
                        _customersDbContext.Customers.Add(customer);
                        _customersDbContext.SaveChanges();
                    }
                    string vinNumber = vehicleObj[int.Parse(headers[5]["headerValue"].ToString())].ToString();
                    Vehicle existingVehicle = _dbContext.Vehicles.SingleOrDefault(x => x.VinNumber == vinNumber);
                    if (existingVehicle == null)
                    {
                        Vehicle vehicle = new Vehicle();

                        //vehicle.ContractNumber = vehicleObj[int.Parse(headers[0]["headerValue"].ToString())].ToString(); // contractNumber
                        //vehicle.CustomerName = vehicleObj[int.Parse(headers[1]["headerValue"].ToString())].ToString(); // CustomerNumber
                        vehicle.Brand = vehicleObj[int.Parse(headers[2]["headerValue"].ToString())].ToString(); // Brand
                        vehicle.ModelName = vehicleObj[int.Parse(headers[3]["headerValue"].ToString())].ToString(); // ModelName
                        vehicle.RegistrationDate = DateTime.UtcNow;// RegistrationDate
                        vehicle.VinNumber = vehicleObj[int.Parse(headers[5]["headerValue"].ToString())].ToString(); // VinNumber
                        //vehicle.ContractStartDate = DateTime.UtcNow; // ContractStartDate
                        //vehicle.ContractEndDate = DateTime.UtcNow; // ContractEndDate

                        _dbContext.Vehicles.Add(vehicle);
                        _dbContext.SaveChanges();
                    }
                    string contractnumber = vehicleObj[int.Parse(headers[0]["headerValue"].ToString())].ToString();
                    VehicleContract existingContract = _dbContext.VehicleContracts.SingleOrDefault(x => x.ContractNumber == contractnumber);
                    if (existingContract == null)
                    {
                        VehicleContract vehicleContract = new VehicleContract();
                        vehicleContract.ContractNumber = vehicleObj[int.Parse(headers[0]["headerValue"].ToString())].ToString();
                        vehicleContract.ContractStartDate = DateTime.Parse(vehicleObj[int.Parse(headers[6]["headerValue"].ToString())].ToString());
                        vehicleContract.ContractEndDate = DateTime.Parse(vehicleObj[int.Parse(headers[7]["headerValue"].ToString())].ToString());
                        vehicleContract.VehicleId = existingVehicle.id;
                        vehicleContract.CustomerId = existingCustomer.Id;

                        _dbContext.VehicleContracts.Add(vehicleContract);
                        _dbContext.SaveChanges();
                    }

                }
                return new OperationResult(true,
                    VehiclePnLocaleHelper.GetString("VehicleCreated"));

                //return new OperationResult(false,
                //                    CustomersPnLocaleHelper.GetString("ErrorWhileCreatingCustomer"));
                /*            throw new NotImplementedException()*/
            }
        }

    }
}