using Microsoft.EntityFrameworkCore;
using ProfileService.Model;

namespace ProfileService.Repository
{
	public interface IAppDbContext
	{
        DbSet<Profile> Profiles { get; set; }
        DbSet<Connection> Connections { get; set; }
        DbSet<Education> Education { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<WorkExperience> WorkExperiences { get; set; }
        DbSet<ConnectionRequest> ConnectionRequests { get; set; }
    }
}

