using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using eFormCore;
using eFormData;
using eFormShared;
using Rebus.Handlers;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Messages;

namespace RentableItems.Pn.Handlers
{
    public class ContractInspectionCreateHandler : IHandleMessages<ContractInspectionCreate>
    {
        private readonly Core _core;
        private readonly RentableItemsPnDbContext _dbContext;

        public ContractInspectionCreateHandler(Core core, RentableItemsPnDbContext context)
        {
            _core = core;
            _dbContext = context;
        }

        public Task Handle(ContractInspectionCreate message)
        {
            throw new System.NotImplementedException();
        }
    }
}