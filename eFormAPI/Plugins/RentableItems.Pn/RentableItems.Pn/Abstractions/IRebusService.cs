using Rebus.Bus;

namespace RentableItems.Pn.Abstractions
{
    public interface IRebusService
    {
        void Start(string connectionString);
        IBus GetBus();
    }
}