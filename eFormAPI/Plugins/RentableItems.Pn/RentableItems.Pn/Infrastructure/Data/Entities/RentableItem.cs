using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class RentableItem : BaseEntity
    {
        public RentableItem()
        {
            this.RentableItemContracts = new HashSet<RentableItemContract>();
        }

        [StringLength(100)]
        public string Brand { get; set; }

        [StringLength(100)]
        public string ModelName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string VinNumber { get; set; }

        public string SerialNumber { get; set; }

        public string PlateNumber { get; set; }

        public virtual ICollection<RentableItemContract> RentableItemContracts { get; set; }
    }
}