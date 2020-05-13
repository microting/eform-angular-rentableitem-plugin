using System.Collections.Generic;

namespace RentableItems.Pn.Infrastructure.Models.Customer
{
    public class CustomersModel
    {
        public int Total { get; set; }
        public List<RentableItemCustomerModel> Customers { get; set; }

        public CustomersModel()
        {
            Customers = new List<RentableItemCustomerModel>();
        }
    }
}
