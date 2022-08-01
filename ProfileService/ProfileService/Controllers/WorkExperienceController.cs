using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Prometheus;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "work experience counter");

        public WorkExperienceController(ITracer tracer)
        {
            _tracer = tracer;
        }

    }
}