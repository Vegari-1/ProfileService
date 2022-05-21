using ProfileService.Model;
using ProfileService.Repository.Interface;

namespace ProfileService.Repository
{
    public class ConnectionRequestRepository : Repository<ConnectionRequest>, IConnectionRequestRepository
    {
        public ConnectionRequestRepository(AppDbContext context) : base(context) { }
    }
}

