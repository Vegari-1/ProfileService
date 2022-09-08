using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using System;
using System.Threading.Tasks;
using ProfileService.Service.Interface.Exceptions;
using System.Collections.Generic;

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

        public async Task<IEnumerable<Profile>> GetByPublic(bool isPublic)
        {
            return await _profileRepository.GetByPublic(isPublic);
        }

        public async Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query)
        {
            return await _profileRepository.GetByPublicAndQuery(isPublic, query);
        }
    }
}