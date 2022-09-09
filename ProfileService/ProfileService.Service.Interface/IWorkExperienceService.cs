using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service.Interface
{
	public interface IWorkExperienceService
	{
		Task<WorkExperience> Create(Guid profileId, WorkExperience workExp);
		Task Delete(Guid profileId, Guid id);
	}
}