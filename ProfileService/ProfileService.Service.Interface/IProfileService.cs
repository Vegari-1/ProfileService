using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Model;

namespace ProfileService.Service.Interface
{
	public interface IProfileService
	{
        Task<Profile> Create(Profile profile);
        Task<Profile> GetById(Guid id);
        Task<Tuple<Profile, int>> GetByIdForProfile(Guid id, Guid profileId);
        Task<IEnumerable<Skill>> GetByIdSkills(Guid id);
        Task<IEnumerable<Education>> GetByIdEducation(Guid id);
        Task<IEnumerable<WorkExperience>> GetByIdWorkExperience(Guid id);
        Task<IEnumerable<Profile>> GetByPublic(bool isPublic);
        Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query);
        Task<IEnumerable<Profile>> GetByNotBlocked(Guid profileId);
        Task<IEnumerable<Profile>> GetByQueryAndNotBlocked(string query, Guid profileId);
        Task<Profile> Update(Guid id, Profile profile);
        Task<Block> Block(Guid id, Guid blockProfileId);
    }
}