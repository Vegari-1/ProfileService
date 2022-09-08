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
    public class SkillController : ControllerBase
    {

        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "profile counter");

        public SkillController(ISkillService skillService, IMapper mapper, ITracer tracer)
        {
            _skillService = skillService;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(
            [FromHeader(Name = "profile-id")][Required] Guid id,
            [FromBody] SkillRequest skillRequest)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create skill");
            counter.Inc();

            Skill skill = await _skillService.Create(id, _mapper.Map<Skill>(skillRequest));

            SkillResponse skillResponse = _mapper.Map<SkillResponse>(skill);

            return Ok(skillResponse);
        }
    }
}