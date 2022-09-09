using ProfileService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileService.Service.Interface
{
    public interface IConnectionRequestService
    {
        Task<Connection> Accept(Guid profileId, Guid id);
        Task Decline(Guid profileId, Guid id);
        Task<Tuple<IEnumerable<ConnectionRequest>, IEnumerable<Profile>>> GetByProfile(Guid profileId);
    }
}