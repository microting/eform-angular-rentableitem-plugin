﻿using System.Threading.Tasks;
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
        [Route("api/rentable-items-pn/settings")]
        public async Task<OperationDataResult<RentableItemBaseSettings>> GetSettings()
        {
            return await _rentableItemsSettingsService.GetSettings();
        }

        [HttpPost]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/rentable-items-pn/settings")]
        public async Task<OperationResult> UpdateSettings([FromBody]RentableItemBaseSettings rentableItemsSettingsModel)
        {
            return await _rentableItemsSettingsService.UpdateSettings(rentableItemsSettingsModel);
        }
    }
}