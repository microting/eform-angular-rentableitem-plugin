using System.Threading.Tasks;
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
        [AllowAnonymous]
        [Route("api/inspections")]
        public async Task<OperationDataResult<ContractInspectionsModel>> GetAllContracts([FromBody] ContractInspectionsRequestModel requestModel)
        {
            return await _contractsInspectionService.GetAllContractInspections(requestModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/inspections/create-inspection")]
        public async Task<OperationResult> CreateContractInspection([FromBody] ContractInspectionModel contractInspectionCreateModel)
        {
            return await _contractsInspectionService.CreateContractInspection(contractInspectionCreateModel);
        }

        [HttpPost]
        [Route("api/inspections/update-inspection")]
        public async Task<OperationResult> UpdateContractInspection([FromBody] ContractInspectionModel contractInspectionUpdateModel)
        {
            return await _contractsInspectionService.UpdateContractInspection(contractInspectionUpdateModel);
        }

        [HttpDelete]
        [Route("api/inspections/delete-inspection")]
        public async Task<OperationResult> DeleteContractInspection([FromBody] ContractInspectionModel contractinspectionDeleteModel)
        {
            return await _contractsInspectionService.DeleteContractInspection(contractinspectionDeleteModel);
        }
    }
}
