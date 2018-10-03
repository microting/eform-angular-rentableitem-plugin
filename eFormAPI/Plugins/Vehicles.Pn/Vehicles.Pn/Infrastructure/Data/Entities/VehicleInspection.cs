using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eFormApi.BasePn.Infrastructure.Data.Base;

namespace Vehicles.Pn.Infrastructure.Data.Entities
{
    public class VehicleInspection : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string WorkflowState { get; set; }

        public int? Version { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DoneAt { get; set; }

        public int EformId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
