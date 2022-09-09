using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProfileService.Repository
{
    public class ConnectionRepository : Repository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(AppDbContext context) : base(context) { }

        public async Task<Connection> GetByProfileIdAndLinkId(Guid profileId, Guid profileLinkId)
        {
            return await _context.Connections
                                .Where(x =>
                                    (x.Profile1 == profileId && x.Profile2 == profileLinkId)
                                    || (x.Profile1 == profileLinkId && x.Profile2 == profileId))
                                .FirstOrDefaultAsync();
        }
    }
}

