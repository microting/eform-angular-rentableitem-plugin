using System;
using Newtonsoft.Json.Linq;
using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Infrastructure.Helpers
{
    public static class ContractsHelper
    {
        public static ContractModel ComposeValues(ContractModel contract, JToken headers,
            JToken rentableObj)
        {
            if (int.TryParse(headers[0]["headerValue"].ToString(), out var locationId))
            {
                contract.ContractNr = (int)rentableObj[locationId]; // Brand
            }

            if (int.TryParse(headers[6]["headerValue"].ToString(), out locationId))
            {
                contract.ContractStart = (DateTime?)rentableObj[locationId]; // Vin Number
            }
            
            if (int.TryParse(headers[7]["headerValue"].ToString(), out locationId))
            {
                contract.ContractEnd = (DateTime?)rentableObj[locationId]; // Vin Number
            }
            
            return contract;
        }
    }
}