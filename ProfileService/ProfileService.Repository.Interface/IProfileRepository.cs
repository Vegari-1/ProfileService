using ProfileService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IProfileRepository : IRepository<Profile>
	{
		Task<Profile> GetById(Guid id);
        Task<Profile> GetByIdImage(Guid id);
        Task<Profile> GetByIdSkills(Guid id);
        Task<Profile> GetByIdEducation(Guid id);
        Task<Profile> GetByIdWorkExperiences(Guid id);
        Task<Profile> GetByIdBlocks(Guid id);
        Task<Profile> GetByUsername(string username);
        Task<IEnumerable<Profile>> GetByPublic(bool isPublic);
        Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query);
        Task<IEnumerable<Profile>> GetByIdList(IEnumerable<Guid> idList);
        Task<IEnumerable<Profile>> GetByNotBlocked(Guid profileId);
        Task<IEnumerable<Profile>> GetByQueryAndNotBlocked(string query, Guid profileId);
    }
}