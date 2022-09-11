using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using System;
using System.Threading.Tasks;
using ProfileService.Service.Interface.Exceptions;
using System.Collections.Generic;
using System.Linq;
using BusService;

namespace ProfileService.Service
{
	public class ProfileService : IProfileService
    {
        private readonly IConnectionRequestRepository _connectionRequestRepository;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileSyncService _profileSyncService;
        private readonly IBlockSyncService _blockSyncService;

        public ProfileService(IConnectionRequestRepository connectionRequestRepository,
            IConnectionRepository connectionRepository, IProfileRepository profileRepository,
            IProfileSyncService profileSyncService, IBlockSyncService blockSyncService)
        {
            _connectionRequestRepository = connectionRequestRepository;
            _connectionRepository = connectionRepository;
            _profileRepository = profileRepository;
            _profileSyncService = profileSyncService;
            _blockSyncService = blockSyncService;
        }

        public async Task<Profile> Create(Profile profile)
        {
            if (await _profileRepository.GetByUsername(profile.Username) != null)
                throw new EntityExistsException(typeof(Profile), "username");

            await _profileRepository.Save(profile);

            _profileSyncService.PublishAsync(profile, Events.Created);

            return profile;
        }

        public async Task<Profile> GetById(Guid id)
        {
            Profile profile = await _profileRepository.GetByIdImage(id);
            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }
            return profile;
        }

        public async Task<Tuple<Profile, int>> GetByIdForProfile(Guid id, Guid profileId)
        {
            Profile profile = await _profileRepository.GetByIdImage(id);
            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }

            int status = 0;
            ConnectionRequest connReq = await _connectionRequestRepository.GetByProfileIdAndLinkId(profileId, id);
            if (connReq != null)
                status = 1;
            Connection conn = await _connectionRepository.GetByProfileIdAndLinkId(profileId, id);
            if (conn != null)
                status = 2;

            return new Tuple<Profile, int>(profile, status);
        }

        public async Task<IEnumerable<Skill>> GetByIdSkills(Guid id)
        {
            Profile profile = await _profileRepository.GetByIdSkills(id);
            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }
            return profile.Skills;
        }

        public async Task<IEnumerable<Education>> GetByIdEducation(Guid id)
        {
            Profile profile = await _profileRepository.GetByIdEducation(id);
            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }
            return profile.Education;
        }
        public async Task<IEnumerable<WorkExperience>> GetByIdWorkExperience(Guid id)
        {
            Profile profile = await _profileRepository.GetByIdWorkExperiences(id);
            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }
            return profile.WorkExperiences;
        }

        public async Task<IEnumerable<Profile>> GetByPublic(bool isPublic)
        {
            return await _profileRepository.GetByPublic(isPublic);
        }

        public async Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query)
        {
            return await _profileRepository.GetByPublicAndQuery(isPublic, query);
        }

        public async Task<IEnumerable<Profile>> GetByNotBlocked(Guid profileId)
        {
            return await _profileRepository.GetByNotBlocked(profileId);
        }

        public async Task<IEnumerable<Profile>> GetByQueryAndNotBlocked(string query, Guid profileId)
        {
            return await _profileRepository.GetByQueryAndNotBlocked(query, profileId);
        }

        public async Task<Profile> Update(Guid id, Profile profile)
        {
            if (!id.Equals(profile.Id))
                throw new ForbiddenException();

            Profile dbProfile = await _profileRepository.GetByIdImage(id);

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

            _profileSyncService.PublishAsync(dbProfile, Events.Updated);

            return dbProfile;
        }

        public async Task<Block> Block(Guid id, Guid blockProfileId)
        {
            Profile blocker = await _profileRepository.GetByIdBlocks(id);
            if (blocker == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }

            Profile blocked = await _profileRepository.GetByIdBlocks(blockProfileId);
            if (blocked == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }

            if (blocker.Blocked.Where(b => b.BlockedId == blockProfileId).FirstOrDefault() != null)
                throw new BadRequestException(typeof(Block), "block profile id");
            if (blocker.BlockedBy.Where(b => b.BlockerId == blockProfileId).FirstOrDefault() != null)
                throw new BadRequestException(typeof(Block), "block profile id");

            Block block = new Block
            {
                BlockerId = blocker.Id,
                Blocker = blocker,
                BlockedId = blocked.Id,
                Blocked = blocked
            };

            blocker.Blocked.Add(block);
            await _profileRepository.SaveChanges();

            _blockSyncService.PublishAsync(block, Events.Created);

            return block;
        }

    }
}