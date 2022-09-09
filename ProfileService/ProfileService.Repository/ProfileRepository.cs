using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Collections.Generic;
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

        public async Task<Profile> GetByIdSkills(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.Skills)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByUsername(string username)
        {
            return await _context.Profiles
                                .Where(x => x.Username == username)
                                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Profile>> GetByPublic(bool isPublic)
        {
            return await _context.Profiles
                                .Where(x => x.Public == isPublic)
                                .Include(x => x.Image)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query)
        {
            return await _context.Profiles
                                .Where(x => x.Public == isPublic)
                                .Where(x =>
                                    x.Username.ToLower().Contains(query.ToLower())
                                    || x.Name.ToLower().Contains(query.ToLower())
                                    || x.Surname.ToLower().Contains(query.ToLower())
                                )
                                .Include(x => x.Image)
                                .ToListAsync();
        }
    }
}

