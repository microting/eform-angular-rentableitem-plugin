using RentableItems.Pn.Infrastructure.Enums;

namespace RentableItems.Pn.Infrastructure.Models.Fields
{
    public class RentableItemsFieldUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldStatus FieldStatus { get; set; }
    }
}