using System;
using System.Threading.Tasks;
using ProfileService.Model;

namespace ProfileService.Service.Interface
{
	public interface IProfileService
	{
		Task<Profile> GetById(Guid id);
	}
}