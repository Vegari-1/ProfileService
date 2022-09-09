using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(AppDbContext context) : base(context) { }

        public async Task<Skill> GetById(Guid id)
        {
            return await _context.Skills
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public async Task<Skill> GetByProfileIdAndName(Guid profileId, string name)
        {
            return await _context.Skills
                                   .Where(x => x.Name.ToLower() == name.ToLower())
                                   .Where(x => x.Profile.Id == profileId)
                                   .FirstOrDefaultAsync();
        }
    }
}

