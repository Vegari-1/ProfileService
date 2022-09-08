using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Model;

namespace ProfileService.Service.Interface
{
	public interface IProfileService
	{
		Task<Profile> GetById(Guid id);
        Task<IEnumerable<Profile>> GetByPublic(bool isPublic);
        Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query);
    }
}