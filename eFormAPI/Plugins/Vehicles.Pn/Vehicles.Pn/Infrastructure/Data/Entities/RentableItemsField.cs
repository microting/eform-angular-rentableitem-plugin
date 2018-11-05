using RentableItems.Pn.Infrastructure.Enums;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace RentableItems.Pn.Infrastructure.Data.Entities
{
    public class RentableItemsField : BaseEntity
    {
        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        public FieldStatus FieldStatus { get; set; }
    }
}
