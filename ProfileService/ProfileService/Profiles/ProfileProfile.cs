using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        
        public ProfileProfile()
        {
            // Source -> Target
            CreateMap<ProfileRequest, Profile>()
                .ForMember(dest => dest.Image, src => src.MapFrom(s => s.Picture));
            CreateMap<Profile, ProfileResponse>();
            CreateMap<Profile, ProfileSimpleResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content));
        }
    }
}
