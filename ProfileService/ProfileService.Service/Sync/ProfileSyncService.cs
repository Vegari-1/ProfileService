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
    public class ProfileSyncService : ConsumerBase<Profile, ProfileContract>, IProfileSyncService
    {
        private readonly IMessageBusService _messageBusService;

        public ProfileSyncService(IMessageBusService messageBusService, ILogger<ProfileSyncService> logger) : base(logger)
        {
            _messageBusService = messageBusService;
        }

        public override Task PublishAsync(Profile entity, string action)
        {
            string avatar = null;
            if (entity.Image != null)
                avatar = entity.Image.Content;
            ProfileContract contract = new(entity.Id, entity.Name, entity.Surname, 
                entity.Email, entity.Username, avatar);

            var serialized = JsonConvert.SerializeObject(contract);
            var bData = Encoding.UTF8.GetBytes(serialized);
            _messageBusService.PublishEvent(SubjectBuilder.Build(Topics.Profile, action), bData);
            return Task.CompletedTask;
        }

        public override Task SynchronizeAsync(ProfileContract entity, string action)
        {
            throw new NotImplementedException();
        }
    }
}
