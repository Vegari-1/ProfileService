using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class SkillProfile : AutoMapper.Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillRequest, Skill>();
            CreateMap<Skill, SkillResponse>();
        }
    }
}
