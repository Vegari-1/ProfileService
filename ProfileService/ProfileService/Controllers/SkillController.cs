using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Prometheus;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "profile counter");

        public SkillController(ITracer tracer)
        {
            _tracer = tracer;
        }
    }
}