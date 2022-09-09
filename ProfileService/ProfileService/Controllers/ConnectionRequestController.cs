using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using ProfileService.Dto;
using ProfileService.Model;
using ProfileService.Service.Interface;
using Prometheus;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfileService
{
    [Route("api/conn-req")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly IConnectionRequestService _connectionRequestService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "connection request counter");

        public ConnectionRequestController(IConnectionRequestService connectionRequestService, 
            IMapper mapper, ITracer tracer)
        {
            _connectionRequestService = connectionRequestService;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileConnectionRequests(
            [FromHeader(Name = "profile-id")][Required] Guid profileId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("get profile connection requests");
            counter.Inc();

            Tuple<IEnumerable<ConnectionRequest>, IEnumerable<Model.Profile>> connReqProfileTuple = 
                await _connectionRequestService.GetByProfile(profileId);

            IEnumerable<ConnectionRequestResponse> connectionRequestResponses =
                _mapper.Map<IEnumerable<ConnectionRequestResponse>>(connReqProfileTuple.Item2);
            IEnumerable<ConnectionRequest> connReqs = connReqProfileTuple.Item1;

            for (int i = 0; i < connectionRequestResponses.Count(); i++)
            {
                connectionRequestResponses.ElementAt(i).Id = connReqs.ElementAt(i).Id;
            }
            return Ok(connectionRequestResponses);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AcceptConnectionRequest(
            [FromHeader(Name = "profile-id")][Required] Guid profileId, Guid id)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("accept connection request");
            counter.Inc();

            Connection connection = await _connectionRequestService.Accept(profileId, id);

            return new ObjectResult(connection) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeclineConnectionRequest(
            [FromHeader(Name = "profile-id")][Required] Guid profileId, Guid id)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("decline connection request");
            counter.Inc();

            await _connectionRequestService.Decline(profileId, id);

            return NoContent();
        }

    }
}