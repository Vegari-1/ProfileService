using ProfileService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IProfileRepository : IRepository<Profile>
	{
		Task<Profile> GetById(Guid id);
        Task<IEnumerable<Profile>> GetByPublic(bool isPublic);
        Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query);
    }
}