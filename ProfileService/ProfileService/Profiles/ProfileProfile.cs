using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        
        public ProfileProfile()
        {
            // Source -> Target
            CreateMap<ProfileRequest, Profile>();
            CreateMap<UpdateProfileRequest, Profile>();
            CreateMap<Profile, ProfileResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content));
            CreateMap<Profile, ProfileSimpleResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content));
            CreateMap<Profile, ConnectionRequestResponse>()
                .ForMember(dest => dest.Picture, src => src.MapFrom(s => s.Image.Content))
                .ForMember(dest => dest.ProfileId, src => src.MapFrom(s => s.Id));
        }
    }
}
