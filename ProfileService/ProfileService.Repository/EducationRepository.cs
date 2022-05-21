using ProfileService.Model;
using ProfileService.Repository.Interface;

namespace ProfileService.Repository
{
    public class EducationRepository : Repository<Education>, IEducationRepository
    {
        public EducationRepository(AppDbContext context) : base(context) { }
    }
}

