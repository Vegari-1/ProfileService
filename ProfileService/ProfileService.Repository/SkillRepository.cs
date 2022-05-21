using ProfileService.Model;
using ProfileService.Repository.Interface;

namespace ProfileService.Repository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(AppDbContext context) : base(context) { }
    }
}

