using System.Collections.Generic;

namespace RentableItems.Pn.Infrastructure.Models.Fields
{
    public class RentableItemsFieldsUpdateModel
    {
        public List<RentableItemsFieldUpdateModel> Fields { get; set; } = new List<RentableItemsFieldUpdateModel>();
    }
}