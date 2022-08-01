using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Prometheus;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "education counter");

        public EducationController(ITracer tracer)
        {
            _tracer = tracer;
        }

    }
}