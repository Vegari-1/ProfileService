using Microsoft.EntityFrameworkCore;
using ProfileService.Model;

namespace ProfileService.Repository
{
	public interface IAppDbContext
	{
        DbSet<Profile> Profile { get; set; }
        DbSet<Education> Education { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<WorkExperience> WorkExperience { get; set; }
        DbSet<ConnectionRequest> ConnectionRequests { get; set; }
    }
}

