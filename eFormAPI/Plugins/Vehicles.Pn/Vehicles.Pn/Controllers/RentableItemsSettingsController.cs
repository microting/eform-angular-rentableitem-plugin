using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Controllers
{
    [Authorize]
    public class RentableItemsSettingsController : Controller
    {
        private readonly IRentableItemsSettingsService _rentableItemsSettingsService;

        public RentableItemsSettingsController(IRentableItemsSettingsService rentableItemsSettingsService)
        {
            _rentableItemsSettingsService = rentableItemsSettingsService;
        }

        [HttpGet]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/rentable-items/settings")]
        public OperationDataResult<RentableItemsSettingsModel> GetSettings()
        {
            return _rentableItemsSettingsService.GetSettings();
        }

        [HttpPost]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/rentable-items/settings")]
        public OperationResult UpdateSettings(RentableItemsSettingsModel rentableItemsSettingsModel)
        {
            return _rentableItemsSettingsService.UpdateSettings(rentableItemsSettingsModel);
        }
    }
}
