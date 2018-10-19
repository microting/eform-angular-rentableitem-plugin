using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Pn.Infrastructure.Data.Entities;

namespace Vehicles.Pn.Helpers
{
    public static class VehiclePnDbHelper
    {
        public static VehicleVersions MapVehicleVersions(Vehicle vehicle)
        {
            VehicleVersions vehicleVer = new VehicleVersions();
            vehicleVer.Brand = vehicle.Brand;
            vehicleVer.created_at = vehicle.created_at;
            vehicleVer.Created_By_User_Id = vehicle.Created_By_User_Id;
            vehicleVer.ModelName = vehicle.ModelName;
            vehicleVer.PlateNumber = vehicle.PlateNumber;
            vehicleVer.RegistrationDate = vehicle.RegistrationDate;
            vehicleVer.updated_at = vehicle.updated_at;
            vehicleVer.Updated_By_User_Id = vehicle.Updated_By_User_Id;
            vehicleVer.VehicleId = vehicle.id;
            vehicleVer.version = vehicle.version;
            vehicleVer.VinNumber = vehicle.VinNumber;
            vehicleVer.workflow_state = vehicle.workflow_state;

            return vehicleVer;
        }

        public static VehicleContractVersions MapVehicleContractVersions(VehicleContract vehicleContract)
        {
            VehicleContractVersions vehicleContractVer = new VehicleContractVersions();
            vehicleContractVer.ConstractEndDate = vehicleContract.ConstractEndDate;
            vehicleContractVer.ConstractStartDate = vehicleContract.ConstractStartDate;
            vehicleContractVer.ContractNumber = vehicleContract.ContractNumber;
            vehicleContractVer.created_at = vehicleContract.created_at;
            vehicleContractVer.Created_By_User_Id = vehicleContract.Created_By_User_Id;
            vehicleContractVer.CustomerId = vehicleContract.CustomerId;
            vehicleContractVer.updated_at = vehicleContract.updated_at;
            vehicleContractVer.Updated_By_User_Id = vehicleContract.Updated_By_User_Id;
            vehicleContractVer.VehicleContractId = vehicleContract.id;
            vehicleContractVer.VehicleId = vehicleContract.VehicleId;
            vehicleContractVer.version = vehicleContract.version;
            vehicleContractVer.workflow_state = vehicleContract.workflow_state;

            return vehicleContractVer;
        }

        public static VehicleInspectionVersions MapVehicleInspectionVersions(VehicleInspection vehicleInspection)
        {
            VehicleInspectionVersions vehicleInspectionVer = new VehicleInspectionVersions();
            vehicleInspectionVer.created_at = vehicleInspection.created_at;
            vehicleInspectionVer.Created_By_User_Id = vehicleInspection.Created_By_User_Id;
            vehicleInspectionVer.DoneAt = vehicleInspection.DoneAt;
            vehicleInspectionVer.SDK_Case_Id = vehicleInspection.SDK_Case_Id;
            vehicleInspectionVer.Status = vehicleInspection.Status;
            vehicleInspectionVer.updated_at = vehicleInspection.updated_at;
            vehicleInspectionVer.Updated_By_User_Id = vehicleInspection.Updated_By_User_Id;
            vehicleInspectionVer.VehicleContractId = vehicleInspection.VehicleContractId;
            vehicleInspectionVer.VehicleInspectionId = vehicleInspection.id;
            vehicleInspectionVer.version = vehicleInspection.version;
            vehicleInspectionVer.workflow_state = vehicleInspection.workflow_state;

            return vehicleInspectionVer;
        }

        public static VehicleSettingsVersions MapVehicleSettingsVersions(VehicleSettings vehicleSettings)
        {
            VehicleSettingsVersions vehicleSettingsVer = new VehicleSettingsVersions();
            vehicleSettingsVer.created_at = vehicleSettings.created_at;
            vehicleSettingsVer.Created_By_User_Id = vehicleSettings.Created_By_User_Id;
            vehicleSettingsVer.Eform_Id = vehicleSettings.Eform_Id;
            vehicleSettingsVer.updated_at = vehicleSettings.updated_at;
            vehicleSettingsVer.Updated_By_User_Id = vehicleSettings.Updated_By_User_Id;
            vehicleSettingsVer.VerhicleSettingId = vehicleSettings.id;
            vehicleSettingsVer.version = vehicleSettings.version;
            vehicleSettingsVer.workflow_state = vehicleSettings.workflow_state;

            return vehicleSettingsVer;
        }
    }
}
