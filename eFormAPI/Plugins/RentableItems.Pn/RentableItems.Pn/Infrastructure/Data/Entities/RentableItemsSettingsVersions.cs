using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class RentableItemsSettingsVersions : BaseEntity
    {
        public int eFormId { get; set; }

        public int RentableItemsSettingId { get; set; }

    }
}
