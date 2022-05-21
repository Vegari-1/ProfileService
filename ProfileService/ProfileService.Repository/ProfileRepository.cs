using ProfileService.Model;
using ProfileService.Repository.Interface;

namespace ProfileService.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context) { }
    }
}

