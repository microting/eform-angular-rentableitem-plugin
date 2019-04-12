using System.Threading.Tasks;
using eFormCore;
using Rebus.Handlers;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Messages;

namespace RentableItems.Pn.Handlers
{
    public class ContractInspectionDeleteHandler : IHandleMessages<ContractInspectionDelete>
    {
        private readonly Core _core;
        private readonly RentableItemsPnDbContext _dbContext;

        public ContractInspectionDeleteHandler(Core core, RentableItemsPnDbContext dbContext)
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