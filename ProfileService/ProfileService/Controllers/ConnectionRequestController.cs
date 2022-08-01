using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Prometheus;

namespace ProfileService
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "connection request counter");

        public ConnectionRequestController(ITracer tracer)
        {
            _tracer = tracer;
        }
    }
}