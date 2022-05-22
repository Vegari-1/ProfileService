using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class WorkExperienceProfile : AutoMapper.Profile
    {
        public WorkExperienceProfile()
        {
            CreateMap<WorkExperience, WorkExperienceResponse>();
        }
    }
}
