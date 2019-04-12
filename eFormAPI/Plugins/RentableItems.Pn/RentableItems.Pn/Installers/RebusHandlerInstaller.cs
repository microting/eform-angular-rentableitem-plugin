using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Rebus.Handlers;
using RentableItems.Pn.Handlers;
using RentableItems.Pn.Messages;

namespace RentableItems.Pn.Installers
{
    public class RebusHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IHandleMessages<ContractInspectionCreate>>().ImplementedBy<ContractInspectionCreateHandler>().LifestyleTransient());            
            container.Register(Component.For<IHandleMessages<ContractInspectionDelete>>().ImplementedBy<ContractInspectionDeleteHandler>().LifestyleTransient());            
        }
    }
}