using System.Collections.Generic;

namespace RentableItems.Pn.Infrastructure.Models
{
    public class ContractInspectionsModel
    {
        public int Total { get; set; }
        public List<ContractInspectionModel> ContractInspections { get; set; }

        public ContractInspectionsModel()
        {
            ContractInspections = new List<ContractInspectionModel>();
        }
    }
}
