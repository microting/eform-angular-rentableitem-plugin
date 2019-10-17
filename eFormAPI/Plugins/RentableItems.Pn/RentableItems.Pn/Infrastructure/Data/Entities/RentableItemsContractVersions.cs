using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class RentableItemsContractVersions: BaseEntity
    {
        public int RentableItemId { get; set; }

        public int ContractId { get; set; }

        public int RentableItemContractId { get; set; }
    }
}
