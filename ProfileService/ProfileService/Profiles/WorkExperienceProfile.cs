using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class WorkExperienceProfile : AutoMapper.Profile
    {
        public WorkExperienceProfile()
        {
            CreateMap<WorkExperienceRequest, WorkExperience>()
                .ForMember(dest => dest.StartDate, src => src.MapFrom(s => s.StartDate.Date));
            CreateMap<WorkExperience, WorkExperienceResponse>();
        }
    }
}
