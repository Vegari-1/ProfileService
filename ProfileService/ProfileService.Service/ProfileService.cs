using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using System;
using System.Threading.Tasks;
using ProfileService.Service.Interface.Exceptions;

namespace ProfileService.Service
{
	public class ProfileService : IProfileService
	{
        private readonly IProfileRepository _profileRepository;

		public ProfileService(IProfileRepository profileRepository)
		{
            _profileRepository = profileRepository;
		}

        public async Task<Profile> GetById(Guid id)
        {
            Profile profile = await _profileRepository.GetById(id);
            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }
            return profile;
        }
    }
}