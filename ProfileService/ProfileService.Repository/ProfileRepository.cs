using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context) { }

        public async Task<Profile> GetById(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.Education)
                                .Include(x => x.Skills)
                                .Include(x => x.WorkExperiences)
                                .FirstOrDefaultAsync();
        }
    }
}

