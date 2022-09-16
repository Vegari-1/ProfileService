using BusService;
using BusService.Contracts;

namespace ProfileService.Service.Interface
{
    public interface IEventSyncService : ISyncService<EventContract, EventContract>
    {
    }
}
