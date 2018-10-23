using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class Contract : BaseEntity
    {

        public Contract()
        {
            this.ContractInspections = new HashSet<ContractInspection>();
        }
        
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

        public virtual ICollection<ContractInspection> ContractInspections { get; set; }

    }
}
