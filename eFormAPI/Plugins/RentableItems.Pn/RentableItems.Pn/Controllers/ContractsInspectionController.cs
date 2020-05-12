using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    public class ContractsInspectionController : Controller
    {
        private readonly IContractsInspectionService _contractsInspectionService;

        public ContractsInspectionController(IContractsInspectionService contractsInspectionService)
        {
            _contractsInspectionService = contractsInspectionService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/inspections")]
        public async Task<OperationDataResult<ContractInspectionsModel>> Index([FromBody] ContractInspectionsRequestModel requestModel)
        {
            return await _contractsInspectionService.Index(requestModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/inspections/create-inspection")]
        public async Task<OperationResult> Create([FromBody] ContractInspectionModel contractInspectionCreateModel)
        {
            return await _contractsInspectionService.Create(contractInspectionCreateModel);
        }

        [HttpPost]
        [Route("api/inspections/update-inspection")]
        public async Task<OperationResult> Update([FromBody] ContractInspectionModel contractInspectionUpdateModel)
        {
            return await _contractsInspectionService.Update(contractInspectionUpdateModel);
        }

        [HttpDelete]
        [Route("api/inspections/delete-inspection/{id}")]
        public async Task<OperationResult> Delete(int id)
        {
            return await _contractsInspectionService.Delete(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/rentable-items-pn/inspection-results/{inspectionId}")]
        public async Task<IActionResult> DownloadEFormPdf(int inspectionId, string token, string fileType)
        {
            try
            {
                if (string.IsNullOrEmpty(fileType))
                {
                    fileType = "pdf";
                }
                string filePath = await _contractsInspectionService.DownloadEFormPdf(inspectionId, token, fileType);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "application/pdf", Path.GetFileName(filePath));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
