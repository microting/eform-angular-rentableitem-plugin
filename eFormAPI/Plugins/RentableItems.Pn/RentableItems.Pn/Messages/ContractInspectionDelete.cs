using RentableItems.Pn.Infrastructure.Models;

namespace RentableItems.Pn.Messages
{
    public class ContractInspectionDelete
    {
        public ContractInspectionModel ContractInspectionModel { get; protected set; }

        public ContractInspectionDelete(ContractInspectionModel contractInspectionModel)
        {
            ContractInspectionModel = contractInspectionModel;
        }
    }
}