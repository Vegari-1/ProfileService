using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IWorkExperienceRepository : IRepository<WorkExperience>
	{
		Task<WorkExperience> GetById(Guid id);
	}
}