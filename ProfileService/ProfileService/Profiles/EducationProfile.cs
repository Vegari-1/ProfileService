using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class EducationProfile : AutoMapper.Profile
    {
        public EducationProfile()
        {
            CreateMap<Education, EducationResponse>();
        }
    }
}
