using BusService;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using ProfileService.Service.Interface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Service
{
	public class ConnectionRequestService : IConnectionRequestService
    {
        private readonly IConnectionRequestRepository _connectionRequestRepository;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IConnectionSyncService _connectionSyncService;
        public ConnectionRequestService(IConnectionRequestRepository connectionRequestRepository,
            IConnectionRepository connectionRepository, IProfileRepository profileRepository,
            IConnectionSyncService connectionSyncService)
        {
            _connectionRequestRepository = connectionRequestRepository;
            _connectionRepository = connectionRepository;
            _profileRepository = profileRepository;
            _connectionSyncService = connectionSyncService;
        }

        public async Task<Tuple<IEnumerable<ConnectionRequest>, IEnumerable<Profile>>> 
            GetByProfile(Guid profileId)
        {
            IEnumerable<ConnectionRequest> connReqs = 
                await _connectionRequestRepository.GetByProfileId(profileId);
            List<Guid> profileIds = connReqs.Select(x => x.Profile1).ToList();

            IEnumerable<Profile> profiles = await _profileRepository.GetByIdList(profileIds);
            return new Tuple<IEnumerable<ConnectionRequest>, IEnumerable<Profile>>
                (connReqs, profiles);
        }

        public async Task<Connection> Accept(Guid profileId, Guid id)
        {
            ConnectionRequest connReq = await CheckValidity(profileId, id);

            Connection connection = new Connection
            {
                Profile1 = connReq.Profile1,
                Profile2 = profileId
            };

            await _connectionRequestRepository.Delete(connReq);
            await _connectionRepository.Save(connection);

            _connectionSyncService.PublishAsync(connection, Events.Created);

            return connection;
        }

        public async Task Decline(Guid profileId, Guid id)
        {
            ConnectionRequest connReq = await CheckValidity(profileId, id);
            await _connectionRequestRepository.Delete(connReq);
        }

        private async Task<ConnectionRequest> CheckValidity(Guid profileId, Guid id)
        {
            Profile user = await _profileRepository.GetById(profileId);
            if (user == null)
                throw new EntityNotFoundException(typeof(Profile), "id");

            ConnectionRequest connReq = await _connectionRequestRepository.GetById(id);
            if (connReq == null)
                throw new EntityNotFoundException(typeof(ConnectionRequest), "id");

            if (connReq.Profile2 != profileId)
                throw new ForbiddenException();

            return connReq;
        }
    }
}