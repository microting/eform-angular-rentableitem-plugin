using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class ContractVersions : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string WorkflowState { get; set; }

        public int? Version { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int Created_By_User_Id { get; set; }

        public int Updated_By_User_Id { get; set; }

        public DateTime ContractStart { get; set; }

        public DateTime ContractEnd { get; set; }

        public int CustomerId { get; set; }

        public int ContractNr { get; set; }

        public int ContractId { get; set; }
    }
}
