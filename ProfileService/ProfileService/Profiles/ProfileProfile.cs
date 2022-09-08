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
            CreateMap<Profile, ProfileSimpleResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content));
        }
    }
}
