using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class RentableItemsVersions : BaseEntity
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
        [StringLength(100)]
        public string Brand { get; set; }

        [StringLength(100)]
        public string ModelName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string VinNumber { get; set; }

        public string SerialNumber { get; set; }

        public string PlateNumber { get; set; }

        public int RentableItemId { get; set; }
    }
}
