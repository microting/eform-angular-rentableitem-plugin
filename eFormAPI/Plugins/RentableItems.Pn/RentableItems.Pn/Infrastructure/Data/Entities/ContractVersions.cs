using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class ContractVersions : BaseEntity
    {
        public int? Status { get; set; }

        public DateTime? ContractStart { get; set; }

        public DateTime? ContractEnd { get; set; }

        public int CustomerId { get; set; }

        public int? ContractNr { get; set; }

        public int ContractId { get; set; }
    }
}
