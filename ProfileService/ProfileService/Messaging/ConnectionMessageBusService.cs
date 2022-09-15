using BusService;
using BusService.Routing;
using Polly;
using ProfileService.Service.Interface;

namespace ProfileService.Messaging
{
    public class ConnectionMessageBusService : MessageBusHostedService
    {
        public ConnectionMessageBusService(IMessageBusService serviceBus, IServiceScopeFactory serviceScopeFactory) : base(serviceBus, serviceScopeFactory)
        {
        }

        protected override void ConfigureSubscribers()
        {
            var policy = BuildPolicy();
        }

        private Policy BuildPolicy()
        {
            return Policy
                    .Handle<Exception>()
                    .WaitAndRetry(5, _ => TimeSpan.FromSeconds(5), (exception, _, _, _) =>
                    {});
        }
    }
}