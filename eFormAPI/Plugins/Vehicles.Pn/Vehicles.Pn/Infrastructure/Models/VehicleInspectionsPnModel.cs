using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Pn.Infrastructure.Models
{
    public class VehicleInspectionsPnModel
    {
        public int Total { get; set; }
        public List<VehicleInspectionPnModel> Inspections { get; set; }

        public VehicleInspectionsPnModel()
        {
            Inspections = new List<VehicleInspectionPnModel>();
        }

    }
}
