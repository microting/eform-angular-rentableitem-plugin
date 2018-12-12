using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class ContractInspection : BaseEntity
    {
      
        [StringLength(255)]
        public string WorkflowState { get; set; }

        public int Version { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int Created_By_User_Id { get; set; }

        public int Updated_By_User_Id { get; set; }

        public DateTime? DoneAt { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public virtual Contract Contract { get; set; }

        public int SDK_Case_Id { get; set; }

        public int SiteId { get; set; }

    }
}
