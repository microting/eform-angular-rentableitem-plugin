using System.ComponentModel.DataAnnotations;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class Field : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}