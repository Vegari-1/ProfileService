using ProfileService.Model;
using ProfileService.Repository.Interface;

namespace ProfileService.Repository
{
    public class WorkExperienceRepository : Repository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(AppDbContext context) : base(context) { }
    }
}

