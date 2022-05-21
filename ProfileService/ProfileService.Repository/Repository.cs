using ProfileService.Repository.Interface;

namespace ProfileService.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
    }
}

