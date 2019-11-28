using System.Collections.Generic;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemsModel
    {
        public int Total { get; set; }
        public List<RentableItemModel> RentableItems { get; set; }
        public List<int> RentableItemIds { get; set; }

        public RentableItemsModel()
        {
            RentableItems = new List<RentableItemModel>();
            RentableItemIds = new List<int>();
        }
    }
}
