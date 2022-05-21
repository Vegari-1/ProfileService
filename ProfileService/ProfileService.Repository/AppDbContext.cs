using Microsoft.EntityFrameworkCore;
using ProfileService.Model;

namespace ProfileService.Repository
{
	public class AppDbContext : DbContext, IAppDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<ConnectionRequest> ConnectionRequests { get; set; }
    }
}

