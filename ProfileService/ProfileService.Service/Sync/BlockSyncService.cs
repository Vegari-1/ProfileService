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
    public class BlockSyncService : ConsumerBase<Block, ProfileContract>, IBlockSyncService
    {
        private readonly IMessageBusService _messageBusService;

        public BlockSyncService(IMessageBusService messageBusService, ILogger<BlockSyncService> logger) : base(logger)
        {
            _messageBusService = messageBusService;
        }

        public override Task PublishAsync(Block entity, string action)
        {
            Connection connection = new Connection { 
                Id = entity.Id,
                Profile1 = entity.BlockerId,
                Profile2 = entity.BlockedId
            };
                
            var serialized = JsonConvert.SerializeObject(connection);
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
