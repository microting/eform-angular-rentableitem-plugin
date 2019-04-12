using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Messages
{
    public class ContractInspectionCreate
    {
        public ContractInspectionModel ContractInspectionModel { get; protected set; }

        public ContractInspectionCreate(ContractInspectionModel contractInspectionModel)
        {
            ContractInspectionModel = contractInspectionModel;
        }
    }
}