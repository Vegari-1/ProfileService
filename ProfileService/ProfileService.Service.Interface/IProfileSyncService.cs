using BusService;
using BusService.Contracts;
using ProfileService.Model;

namespace ProfileService.Service.Interface
{
    public interface IProfileSyncService : ISyncService<Profile, ProfileContract>
    {
    }
}
