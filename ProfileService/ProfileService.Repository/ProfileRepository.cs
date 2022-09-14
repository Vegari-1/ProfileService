using Microsoft.EntityFrameworkCore;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context) { }

        public async Task<Profile> GetById(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByApiKey(string apiKey)
        {
            return await _context.Profiles
                                .Where(x => x.ApiKey == apiKey)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByIdImage(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.Image)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByIdSkills(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.Skills)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByIdEducation(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.Education)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByIdWorkExperiences(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.WorkExperiences)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByIdBlocks(Guid id)
        {
            return await _context.Profiles
                                .Where(x => x.Id == id)
                                .Include(x => x.Blocked)
                                .Include(x => x.BlockedBy)
                                .FirstOrDefaultAsync();
        }

        public async Task<Profile> GetByUsername(string username)
        {
            return await _context.Profiles
                                .Where(x => x.Username == username)
                                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Profile>> GetByPublic(bool isPublic)
        {
            return await _context.Profiles
                                .Where(x => x.Public == isPublic)
                                .Include(x => x.Image)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetByPublicAndQuery(bool isPublic, string query)
        {
            return await _context.Profiles
                                .Where(x => x.Public == isPublic)
                                .Where(x =>
                                    x.Username.ToLower().Contains(query.ToLower())
                                    || x.Name.ToLower().Contains(query.ToLower())
                                    || x.Surname.ToLower().Contains(query.ToLower())
                                )
                                .Include(x => x.Image)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetByIdList(IEnumerable<Guid> idList)
        {
            return await _context.Profiles
                                .Where(x => idList.Contains(x.Id))
                                .Include(x => x.Image)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetByNotBlocked(Guid profileId)
        {
            List<Guid> blocks = await GetBlocksForProfile(profileId);
            return await _context.Profiles
                                .Where(x => x.Id != profileId)
                                .Where(x => !blocks.Contains(x.Id))
                                .Include(x => x.Image)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> GetByQueryAndNotBlocked(string query, Guid profileId)
        {
            List<Guid> blocks = await GetBlocksForProfile(profileId);
            return await _context.Profiles
                                .Where(x =>
                                    x.Username.ToLower().Contains(query.ToLower())
                                    || x.Name.ToLower().Contains(query.ToLower())
                                    || x.Surname.ToLower().Contains(query.ToLower())
                                )
                                .Where(x => x.Id != profileId)
                                .Where(x => !blocks.Contains(x.Id))
                                .Include(x => x.Image)
                                .ToListAsync();
        }

        private async Task<List<Guid>> GetBlocksForProfile(Guid profileId)
        {
            List<Guid> blocks = await _context.Blocks
                                    .Where(x => x.BlockerId == profileId)
                                    .Select(x => x.BlockedId)
                                    .ToListAsync();
            blocks.AddRange(await _context.Blocks
                                    .Where(x => x.BlockedId == profileId)
                                    .Select(x => x.BlockerId)
                                    .ToListAsync());
            return blocks;
        }

    }
}

