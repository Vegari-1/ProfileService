using BusService;
using BusService.Contracts;
using BusService.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Model;
using ProfileService.Service.Interface;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Service
{
    public class ConnectionSyncService : ConsumerBase<Connection, ProfileContract>, IConnectionSyncService
    {
        private readonly IMessageBusService _messageBusService;

        public ConnectionSyncService(IMessageBusService messageBusService, ILogger<ConnectionSyncService> logger) : base(logger)
        {
            _messageBusService = messageBusService;
        }

        public override Task PublishAsync(Connection entity, string action)
        {
            var serialized = JsonConvert.SerializeObject(entity);
            var bData = Encoding.UTF8.GetBytes(serialized);
            _messageBusService.PublishEvent(SubjectBuilder.Build(Topics.Profile, Events.Updated), bData);
            return Task.CompletedTask;
        }

        public override Task SynchronizeAsync(ProfileContract entity, string action)
        {
            throw new NotImplementedException();
        }
    }
}
