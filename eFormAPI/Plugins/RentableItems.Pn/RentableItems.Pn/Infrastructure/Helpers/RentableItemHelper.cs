using System;
using Newtonsoft.Json.Linq;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Infrastructure.Helpers
{
    public static class RentableItemHelper
    {
        public static RentableItemModel ComposeValues(RentableItemModel rentableItem, JToken headers,
            JToken rentableObj)
        {
            if (int.TryParse(headers[2]["headerValue"].ToString(), out var brandId))
            {
                rentableItem.Brand = rentableObj[brandId].ToString(); // Brand
            }
            
            if (int.TryParse(headers[3]["headerValue"].ToString(), out var modelId))
            {
                rentableItem.ModelName = rentableObj[modelId].ToString(); // Model Name
            }
            
            if (int.TryParse(headers[4]["headerValue"].ToString(), out var regDateId))
            {
                rentableItem.RegistrationDate = DateTime.Parse(rentableObj[regDateId].ToString()); // Model Name
            }
            
            if (int.TryParse(headers[5]["headerValue"].ToString(), out var vinNumberId))
            {
                rentableItem.VinNumber = rentableObj[vinNumberId].ToString(); // Vin Number
            }
            
            return rentableItem;
        }
    }
}