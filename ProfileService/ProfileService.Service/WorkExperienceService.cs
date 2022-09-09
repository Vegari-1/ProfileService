using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using ProfileService.Service.Interface.Exceptions;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service
{
	public class WorkExperienceService : IWorkExperienceService
	{

        private readonly IWorkExperienceRepository _workExpRepository;
        private readonly IProfileRepository _profileRepository;

        public WorkExperienceService(IWorkExperienceRepository workExpRepository, IProfileRepository profileRepository)
        {
            _workExpRepository = workExpRepository;
            _profileRepository = profileRepository;
        }

        public async Task<WorkExperience> Create(Guid profileId, WorkExperience workExp)
        {
            if (workExp.StartDate.Date > DateTime.Now.Date)
            {
                throw new BadRequestException(typeof(WorkExperience), "start date");
            }
            if (workExp.EndDate != null)
            {
                if (workExp.StartDate.Date > workExp.EndDate.Value.Date
                    || workExp.EndDate.Value.Date > DateTime.Now.Date)
                    throw new BadRequestException(typeof(WorkExperience), "end date");
            }

            Profile profile = await _profileRepository.GetByIdWorkExperiences(profileId);
            profile.WorkExperiences.Add(workExp);
            await _profileRepository.SaveChanges();

            return workExp;
        }

        public async Task Delete(Guid profileId, Guid id)
        {
            WorkExperience workExp = await _workExpRepository.GetById(id);
            if (workExp == null)
                throw new EntityNotFoundException(typeof(WorkExperience), "id");
            if (workExp.ProfileId != profileId)
                throw new ForbiddenException();

            await _workExpRepository.Delete(workExp);
        }
    }
}