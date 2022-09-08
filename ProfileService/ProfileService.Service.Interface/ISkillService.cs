using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service.Interface
{
	public interface ISkillService
	{
		Task<Skill> Create(Guid profileId, Skill skill);
	}
}