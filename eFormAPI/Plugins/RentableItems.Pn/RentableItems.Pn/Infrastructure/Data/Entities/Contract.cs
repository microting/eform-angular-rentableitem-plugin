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
        
        public int? Status { get; set; }

        public DateTime? ContractStart { get; set; }

        public DateTime? ContractEnd { get; set; }

        public int CustomerId { get; set; }

        public int? ContractNr { get; set; }

        public virtual ICollection<ContractInspection> ContractInspections { get; set; }

    }
}
