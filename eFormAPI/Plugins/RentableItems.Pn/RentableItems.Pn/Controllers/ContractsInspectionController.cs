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
    [Route("api/rentable-items-pn/inspections")]
    public class ContractsInspectionController : Controller
    {
        private readonly IContractsInspectionService _contractsInspectionService;
        public ContractsInspectionController(IContractsInspectionService contractsInspectionService)
        {
            _contractsInspectionService = contractsInspectionService;
        }

        [HttpGet]
        public Task<OperationDataResult<ContractInspectionsModel>> Index(ContractInspectionsRequestModel requestModel)
        {
            return _contractsInspectionService.Index(requestModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<OperationResult> Create([FromBody] ContractInspectionModel contractInspectionCreateModel)
        {
            return _contractsInspectionService.Create(contractInspectionCreateModel);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<OperationDataResult<ContractInspectionModel>> Read(int id)
        {
            return _contractsInspectionService.Get(id);
        }

        [HttpPut]
        [Route("")]
        public Task<OperationResult> Update([FromBody] ContractInspectionModel contractInspectionUpdateModel)
        {
            return _contractsInspectionService.Update(contractInspectionUpdateModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<OperationResult> Delete(int id)
        {
            return _contractsInspectionService.Delete(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reports/{inspectionId}")]
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
