using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        
        public ProfileProfile()
        {
            // Source -> Target
            CreateMap<Profile, ProfileResponse>();
        }
    }
}
