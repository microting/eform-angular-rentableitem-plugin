using Newtonsoft.Json.Linq;
using RentableItems.Pn.Infrastructure.Models.Customer;

namespace RentableItems.Pn.Infrastructure.Helpers
{
    public static class CustomerHelper
    {
        public static CustomerFullModel ComposeValues(CustomerFullModel customer, JToken headers, JToken customerObj)
        {
            if (int.TryParse(headers[1]["headerValue"].ToString(), out var locationId))
            {
                customer.CompanyName = customerObj[locationId].ToString(); // Cityname
            }

            return customer;
        }
    }
}