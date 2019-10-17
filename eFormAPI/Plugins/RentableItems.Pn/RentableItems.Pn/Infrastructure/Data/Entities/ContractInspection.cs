using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class ContractInspection : BaseEntity
    {
        
        public int? Status { get; set; }
        
        public DateTime? DoneAt { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public int SDKCaseId { get; set; }

        public int SiteId { get; set; }

        public virtual Contract Contract { get; set; }

    }
}
