using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using ProfileService.Model;
using ProfileService.Service.Interface;
using Prometheus;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {

        private readonly IConnectionService _connectionService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "connection counter");

        public ConnectionController(IConnectionService connectionService, IMapper mapper, ITracer tracer)
        {
            _connectionService = connectionService;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpPost]
        [Route("{linkProfileId}")]
        public async Task<IActionResult> CreateConnection(
            [FromHeader(Name = "profile-id")][Required] Guid id,
            Guid linkProfileId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create connection");
            counter.Inc();

            IConnection conn = await _connectionService.Create(id, linkProfileId);

            return new ObjectResult(conn) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete]
        [Route("{linkProfileId}")]
        public async Task<IActionResult> DeleteConnection(
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid linkProfileId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("delete connection");
            counter.Inc();

            await _connectionService.Delete(profileId, linkProfileId);

            return NoContent();
        }
    }
}