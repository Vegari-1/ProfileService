using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository
{
    public class WorkExperienceRepository : Repository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(AppDbContext context) : base(context) { }

        public async Task<WorkExperience> GetById(Guid id)
        {
            return await _context.WorkExperiences
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
    }
}

