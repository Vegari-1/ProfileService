using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using ProfileService.Service.Interface.Exceptions;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service
{
	public class EducationService : IEducationService
	{

        private readonly IEducationRepository _educationRepository;
        private readonly IProfileRepository _profileRepository;

        public EducationService(IEducationRepository educationRepository, IProfileRepository profileRepository)
		{
            _educationRepository = educationRepository;
            _profileRepository = profileRepository;
		}

        public async Task<Education> Create(Guid profileId, Education education)
        {
            if (education.StartDate.Date > DateTime.Now.Date)
            {
                throw new BadRequestException(typeof(Education), "start date");
            }
            if (education.EndDate != null)
            {
                if (education.StartDate.Date > education.EndDate.Value.Date
                    || education.EndDate.Value.Date > DateTime.Now.Date)
                    throw new BadRequestException(typeof(Education), "end date");
            }

            Profile profile = await _profileRepository.GetByIdEducation(profileId);
            profile.Education.Add(education);
            await _profileRepository.SaveChanges();

            return education;
        }

        public async Task Delete(Guid profileId, Guid id)
        {
            Education education = await _educationRepository.GetById(id);
            if (education == null)
                throw new EntityNotFoundException(typeof(Education), "id");
            if (education.ProfileId != profileId)
                throw new ForbiddenException();

            await _educationRepository.Delete(education);
        }
    }
}