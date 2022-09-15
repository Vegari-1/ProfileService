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
    public class BlockSyncService : ConsumerBase<Block, ConnectionContract>, IBlockSyncService
    {
        private readonly IMessageBusService _messageBusService;

        public BlockSyncService(IMessageBusService messageBusService, ILogger<BlockSyncService> logger) : base(logger)
        {
            _messageBusService = messageBusService;
        }

        public override Task PublishAsync(Block entity, string action)
        {
            ConnectionContract connection = new ConnectionContract(entity.Id, entity.BlockerId, entity.BlockedId);

            var serialized = JsonConvert.SerializeObject(connection);
            var bData = Encoding.UTF8.GetBytes(serialized);
            _messageBusService.PublishEvent(SubjectBuilder.Build(Topics.Block, action), bData);
            return Task.CompletedTask;
        }

        public override Task SynchronizeAsync(ConnectionContract entity, string action)
        {
            throw new NotImplementedException();
        }
    }
}
