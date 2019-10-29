using System;
namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public string VinNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PlateNumber { get; set; }
        public string WorkflowState { get; set; }

    }

}
