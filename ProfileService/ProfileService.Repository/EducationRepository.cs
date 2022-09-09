using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository
{
    public class EducationRepository : Repository<Education>, IEducationRepository
    {
        public EducationRepository(AppDbContext context) : base(context) { }

        public async Task<Education> GetById(Guid id)
        {
            return await _context.Education
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
    }
}

