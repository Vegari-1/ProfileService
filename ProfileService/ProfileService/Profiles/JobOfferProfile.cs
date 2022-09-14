using ProfileService.Dto;
using ProfileService.Model;

namespace ProfileService.Profiles
{
    public class JobOfferProfile : AutoMapper.Profile
    {
        public JobOfferProfile()
        {
            CreateMap<JobOfferRequest, JobOffer>();
        }
    }
}
