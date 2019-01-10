using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    public class ContractsInspectionController
    {
        private readonly IContractsInspectionService _contractsInspectionService;

        public ContractsInspectionController(IContractsInspectionService contractsInspectionService)
        {
            _contractsInspectionService = contractsInspectionService;
        }

        [HttpPost]
        [Route("api/inspections")]
        public OperationDataResult<ContractInspectionsModel> GetAllContracts([FromBody] ContractInspectionsRequestModel requestModel)
        {
            return _contractsInspectionService.GetAllContractInspections(requestModel);
        }

        [HttpPost]
        [Route("api/inspections/create-inspection")]
        public OperationResult CreateContractInspection([FromBody] ContractInspectionModel contractInspectionCreateModel)
        {
            return _contractsInspectionService.CreateContractInspection(contractInspectionCreateModel);
        }

        [HttpPost]
        [Route("api/inspections/update-inspection")]
        public OperationResult UpdateContractInspection([FromBody] ContractInspectionModel contractInspectionUpdateModel)
        {
            return _contractsInspectionService.UpdateContractInspection(contractInspectionUpdateModel);
        }

        [HttpDelete]
        [Route("api/inspections/delete-inspection")]
        public OperationResult DeleteContractInspection([FromBody] ContractInspectionModel contractinspectionDeleteModel)
        {
            return _contractsInspectionService.DeleteContractInspection(contractinspectionDeleteModel);
        }
    }
}
