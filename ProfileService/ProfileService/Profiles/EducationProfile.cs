using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class EducationProfile : AutoMapper.Profile
    {
        public EducationProfile()
        {
            CreateMap<EducationRequest, Education>()
                .ForMember(dest => dest.StartDate, src => src.MapFrom(s => s.StartDate.Date));
            CreateMap<Education, EducationResponse>();
        }
    }
}
