using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using ProfileService.Service.Interface.Exceptions;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service
{
	public class ConnectionService : IConnectionService
    {
        private readonly IConnectionRequestRepository _connectionRequestRepository;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IProfileRepository _profileRepository;

        public ConnectionService(IConnectionRequestRepository connectionRequestRepository, 
            IConnectionRepository connectionRepository, IProfileRepository profileRepository)
        {
            _connectionRequestRepository = connectionRequestRepository;
            _connectionRepository = connectionRepository;
            _profileRepository = profileRepository;
        }

        public async Task<IConnection> Create(Guid profileId, Guid linkProfileId)
        {
            if (profileId == linkProfileId)
                throw new BadRequestException(typeof(Connection), "connection profile id");

            Profile user = await _profileRepository.GetById(profileId);
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }

            Profile linkProfile = await _profileRepository.GetById(linkProfileId);
            if (linkProfile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), "id");
            }

            Connection existingConn = await
                    _connectionRepository.GetByProfileIdAndLinkId(profileId, linkProfileId);
            if (existingConn != null)
                throw new EntityExistsException(typeof(Connection), "connection profile id");

            if (linkProfile.Public)
            {
                return await CreatePublic(profileId, linkProfileId);
            } else
            {
                return await CreatePrivate(profileId, linkProfileId);
            }
        }

        private async Task<Connection> CreatePublic(Guid profileId, Guid linkProfileId)
        {
            Connection conn = new Connection
            {
                Profile1 = profileId,
                Profile2 = linkProfileId
            };
            return await _connectionRepository.Save(conn);
        }

        private async Task<ConnectionRequest> CreatePrivate(Guid profileId, Guid linkProfileId)
        {
            ConnectionRequest existingConnReq = await 
                _connectionRequestRepository.GetByProfileIdAndLinkId(profileId, linkProfileId);
            if (existingConnReq != null)
                throw new EntityExistsException(typeof(ConnectionRequest), "connection profile id");

            ConnectionRequest connReq = new ConnectionRequest
            {
                Profile1 = profileId,
                Profile2 = linkProfileId
            };
            return await _connectionRequestRepository.Save(connReq);
        }

        public async Task Delete(Guid profileId, Guid linkProfileId)
        {
            Connection conn = await
                    _connectionRepository.GetByProfileIdAndLinkId(profileId, linkProfileId);
            if (conn == null)
                throw new EntityNotFoundException(typeof(Connection), "connection profile id");

            await _connectionRepository.Delete(conn);
        }
    }
}