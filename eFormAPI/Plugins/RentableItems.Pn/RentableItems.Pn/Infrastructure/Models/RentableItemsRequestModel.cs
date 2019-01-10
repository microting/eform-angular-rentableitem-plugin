namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemsRequestModel
    {
        public string Model { get; set; }
        public string SortColumnName { get; set; }
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public bool IsSortDsc { get; set; }
    }
}
