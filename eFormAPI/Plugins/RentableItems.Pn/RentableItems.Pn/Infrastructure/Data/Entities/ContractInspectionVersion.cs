using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class ContractInspectionVersion : BaseEntity
    {

        public DateTime? DoneAt { get; set; }

        public int SDKCaseId { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public int? Status { get; set; }

        public int ContractInspectionId { get; set; }

        public int SiteId { get; set; }

    }
}
