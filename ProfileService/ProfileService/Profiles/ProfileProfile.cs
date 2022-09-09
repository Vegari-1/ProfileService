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
            CreateMap<UpdateProfileRequest, Profile>();
            CreateMap<Profile, ProfileResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content));
            CreateMap<Profile, ProfileSimpleResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content));
        }
    }
}
