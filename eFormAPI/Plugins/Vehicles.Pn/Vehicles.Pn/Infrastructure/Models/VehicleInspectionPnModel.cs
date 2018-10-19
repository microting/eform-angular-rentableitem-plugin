﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Pn.Infrastructure.Models
{
    public class VehicleInspectionPnModel
    {

        public int Id { get; set; }
        public string WorkflowState { get; set; }
        public int Version { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }
        public DateTime? DoneAt { get; set; }
        public int VehicleContractId { get; set; }
        public int SDKCaseId { get; set; }

    }
}
