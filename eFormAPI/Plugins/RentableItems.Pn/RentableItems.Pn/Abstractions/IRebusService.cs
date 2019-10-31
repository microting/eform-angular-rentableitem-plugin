using System.Threading.Tasks;
using Rebus.Bus;

namespace RentableItems.Pn.Abstractions
{
    public interface IRebusService
    {
        Task Start(string connectionString);
        IBus GetBus();
    }
}