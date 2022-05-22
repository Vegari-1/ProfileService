using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IProfileRepository : IRepository<Profile>
	{
		Task<Profile> GetById(Guid id);
	}
}