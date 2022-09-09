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
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("education_service_counter", "education counter");

        public EducationController(IEducationService educationService, IMapper mapper, ITracer tracer)
        {
            _educationService = educationService;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation(
            [FromHeader(Name = "profile-id")][Required] Guid id,
            [FromBody] EducationRequest educationRequest)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create education");
            counter.Inc();

            Education education= await _educationService.Create(id, _mapper.Map<Education>(educationRequest));

            EducationResponse educationResponse = _mapper.Map<EducationResponse>(education);

            return new ObjectResult(educationResponse) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEducation(
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid id)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("delete education");
            counter.Inc();

            await _educationService.Delete(profileId, id);

            return NoContent();
        }

    }
}