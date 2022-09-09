using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using ProfileService.Dto;
using ProfileService.Model;
using ProfileService.Service.Interface;
using Prometheus;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        private readonly IWorkExperienceService _workExpService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "work experience counter");

        public WorkExperienceController(IWorkExperienceService workExpService, IMapper mapper, ITracer tracer)
        {
            _workExpService = workExpService;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkExp(
            [FromHeader(Name = "profile-id")][Required] Guid id,
            [FromBody] WorkExperienceRequest workExpRequest)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create work experience");
            counter.Inc();

            WorkExperience workExp = await _workExpService.Create(id, _mapper.Map<WorkExperience>(workExpRequest));

            WorkExperienceResponse workExpResponse = _mapper.Map<WorkExperienceResponse>(workExp);

            return new ObjectResult(workExpResponse) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEducation(
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid id)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("delete work experience");
            counter.Inc();

            await _workExpService.Delete(profileId, id);

            return NoContent();
        }

    }
}