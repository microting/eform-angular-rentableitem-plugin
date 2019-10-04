using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class RentableItemContract : BaseEntity
    {
      
        [ForeignKey("RentableItem")]
        public int RentableItemId { get; set; }

        public int ContractId { get; set; }

        public virtual RentableItem RentableItem { get; set; }
    }
}
