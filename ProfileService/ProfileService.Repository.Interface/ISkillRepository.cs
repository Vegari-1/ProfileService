using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface ISkillRepository : IRepository<Skill>
	{
		Task<Skill> GetByProfileIdAndName(Guid id, string name);
	}
}