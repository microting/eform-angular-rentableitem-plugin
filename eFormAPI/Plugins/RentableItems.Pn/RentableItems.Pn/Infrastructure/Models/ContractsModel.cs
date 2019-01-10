using System.Collections.Generic;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class ContractsModel
    {
        public int Total { get; set; }
        public List<ContractModel> Contracts { get; set; }

        public ContractsModel()
        {
            Contracts = new List<ContractModel>();
        }
    }
}
