﻿using ProfileService.Model;
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

        public async Task<Profile> Create(Profile profile)
        {
            if (await _profileRepository.GetByUsername(profile.Username) != null)
                throw new EntityExistsException(typeof(Profile), "username");
            return await _profileRepository.Save(profile);
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

        public async Task<IEnumerable<Skill>> GetByIdSkills(Guid id)
        {
            return (await _profileRepository.GetByIdSkills(id)).Skills;
        }

        public async Task<IEnumerable<Profile>> GetByPublic(bool isPublic)
        {
            return await _profileRepository.GetByPublic(isPublic);
        }

        public async Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query)
        {
            return await _profileRepository.GetByPublicAndQuery(isPublic, query);
        }

        public async Task<Profile> Update(Guid id, Profile profile)
        {
            if (!id.Equals(profile.Id))
                throw new ForbiddenException();

            Profile dbProfile = await _profileRepository.GetById(id);

            dbProfile.Public = profile.Public;
            dbProfile.Name = profile.Name;
            dbProfile.Surname = profile.Surname;
            dbProfile.Username = profile.Username;
            dbProfile.Email = profile.Email;
            dbProfile.Phone = profile.Phone;
            dbProfile.Gender = profile.Gender;
            dbProfile.DateOfBirth = profile.DateOfBirth.Date;
            dbProfile.Biography = profile.Biography;

            await _profileRepository.SaveChanges();

            return dbProfile;
        }
    }
}