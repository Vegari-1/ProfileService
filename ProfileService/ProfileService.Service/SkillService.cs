using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using ProfileService.Service.Interface.Exceptions;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service
{
	public class SkillService : ISkillService
	{
        private readonly ISkillRepository _skillRepository;
        private readonly IProfileRepository _profileRepository;

        public SkillService(ISkillRepository skillRepository, IProfileRepository profileRepository)
		{
            _skillRepository = skillRepository;
            _profileRepository = profileRepository;
		}

        public async Task<Skill> Create(Guid profileId, Skill skill)
        {
            Skill existingSkill = await _skillRepository.GetByProfileIdAndName(profileId, skill.Name);
            if (existingSkill != null)
                throw new EntityExistsException(typeof(Skill), "name");

            Profile profile = await _profileRepository.GetById(profileId);
            profile.Skills.Add(skill);
            await _profileRepository.SaveChanges();

            return skill;
        }

        public async Task Delete(Guid profileId, Guid id)
        {
            Skill skill = await _skillRepository.GetById(id);
            if (skill == null)
                throw new EntityNotFoundException(typeof(Skill), "id");
            if (skill.ProfileId != profileId)
                throw new ForbiddenException();

            await _skillRepository.Delete(skill);
        }
    }
}