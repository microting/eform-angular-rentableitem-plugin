using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class ContractInspectionVersion : BaseEntity
    {
       
        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        [StringLength(255)]
        public string Workflow_state { get; set; }

        public int Version { get; set; }

        public int Created_By_User_Id { get; set; }

        public int Updated_By_User_Id { get; set; }

        public DateTime? DoneAt { get; set; }

        public int SDK_Case_Id { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public int? Status { get; set; }

        public int ContractInspectionId { get; set; }

        public int SiteId { get; set; }

    }
}
