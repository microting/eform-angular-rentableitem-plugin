using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eFormApi.BasePn.Infrastructure.Data.Base;

namespace Vehicles.Pn.Infrastructure.Data.Entities
{
    public class VehicleSettingsVersions : BaseEntity
    {

        [Key]
        public int id { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [StringLength(255)]
        public string workflow_state { get; set; }

        public int version { get; set; }

        public int Created_By_User_Id { get; set; }

        public int Updated_By_User_Id { get; set; }

        public int Eform_Id { get; set; }

        public int VerhicleSettingId { get; set; }
    }
}
