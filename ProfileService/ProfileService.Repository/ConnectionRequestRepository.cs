using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository
{
    public class ConnectionRequestRepository : Repository<ConnectionRequest>, IConnectionRequestRepository
    {
        public ConnectionRequestRepository(AppDbContext context) : base(context) { }

        public async Task<ConnectionRequest> GetByProfileIdAndLinkId(Guid profileId, Guid profileLinkId)
        {
            return await _context.ConnectionRequests
                                .Where(x =>
                                    (x.Profile1 == profileId && x.Profile2 == profileLinkId)
                                    || (x.Profile1 == profileLinkId && x.Profile2 == profileId))
                                .FirstOrDefaultAsync();
        }
    }
}

