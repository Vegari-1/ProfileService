using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service.Interface
{
	public interface IEducationService
	{
		Task<Education> Create(Guid profileId, Education education);
		Task Delete(Guid profileId, Guid id);
	}
}