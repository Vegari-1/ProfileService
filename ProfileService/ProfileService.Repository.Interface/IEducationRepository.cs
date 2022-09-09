using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IEducationRepository : IRepository<Education>
	{
		Task<Education> GetById(Guid id);
	}
}