using System.Threading.Tasks;
using eFormCore;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Rebus.Handlers;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Messages;

namespace RentableItems.Pn.Handlers
{
    public class ContractInspectionDeleteHandler : IHandleMessages<ContractInspectionDelete>
    {
        private readonly Core _core;
        private readonly eFormRentableItemPnDbContext _dbContext;

        public ContractInspectionDeleteHandler(Core core, eFormRentableItemPnDbContext dbContext)
        {
            _core = core;
            _dbContext = dbContext;
        }
        
        public Task Handle(ContractInspectionDelete message)
        {
            throw new System.NotImplementedException();
        }
    }
}