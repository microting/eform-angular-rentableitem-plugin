using System;
using System.Threading.Tasks;
using eFormCore;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Rebus.Handlers;
using RentableItems.Pn.Messages;

namespace RentableItems.Pn.Handlers
{
    public class ContractInspectionCreateHandler : IHandleMessages<ContractInspectionCreate>
    {
        private readonly Core _core;
        private readonly eFormRentableItemPnDbContext _dbContext;

        public ContractInspectionCreateHandler(Core core, eFormRentableItemPnDbContext context)
        {
            _core = core;
            _dbContext = context;
        }

        public Task Handle(ContractInspectionCreate message)
        {
            throw new NotImplementedException();
        }
    }
}