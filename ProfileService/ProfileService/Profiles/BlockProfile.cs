using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class BlockProfile : AutoMapper.Profile
    {
        public BlockProfile()
        {
            CreateMap<Block, BlockResponse>();
        }
    }
}
