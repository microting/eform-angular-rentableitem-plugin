using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    [Route("api/rentable-items-pn/import")]
    public class ImportsController : Controller
    {
        private readonly IImportsService _importsService;

        public ImportsController(IImportsService importsService)
        {
            _importsService = importsService;
        }

        [HttpPost]
        [Route("")]
        public async Task<OperationResult> Import([FromBody] ImportModel importModel)
        {
            return await _importsService.Import(importModel);
        }
    }
}