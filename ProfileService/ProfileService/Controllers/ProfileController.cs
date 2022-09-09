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
    public class ProfileController : ControllerBase
    {

        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("profile_service_counter", "profile counter");

        public ProfileController(IProfileService profileSerivce, IMapper mapper, ITracer tracer)
        {
            _profileService = profileSerivce;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpGet]
        public async Task<IActionResult> SearchPublicProfiles(bool isPublic, string? query)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("search public profiles");
            counter.Inc();

            IEnumerable<Model.Profile> profiles;
            if (query != null)
            {
                profiles = await _profileService.GetByPublicAndQuery(isPublic, query);
            } else
            {
                profiles = await _profileService.GetByPublic(isPublic);
            }

            IEnumerable<ProfileSimpleResponse> profilesResponse =
                _mapper.Map<IEnumerable<ProfileSimpleResponse>>(profiles);

            return Ok(profilesResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileRequest profileRequest)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create profile");
            counter.Inc();

            Model.Profile profile = await _profileService.Create(_mapper.Map<Model.Profile>(profileRequest));

            ProfileResponse profileResponse = _mapper.Map<ProfileResponse>(profile);

            return new ObjectResult(profileResponse) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(
            [FromHeader(Name = "profile-id")][Required] Guid id,
            [FromBody] UpdateProfileRequest updateProfileRequest)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("edit profile");
            counter.Inc();

            Model.Profile profile = await _profileService.Update(id, _mapper.Map<Model.Profile>(updateProfileRequest));

            ProfileResponse profileResponse = _mapper.Map<ProfileResponse>(profile);

            return Ok(profileResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            Model.Profile profile = await _profileService.GetById(id);

            ProfileResponse profileResponse = _mapper.Map<ProfileResponse>(profile);

            return Ok(profileResponse);
        }

        [HttpGet]
        [Route("{id}/skill")]
        public async Task<IActionResult> GetProfileSkills(Guid id)
        {
            IEnumerable<Skill> skills = await _profileService.GetByIdSkills(id);

            IEnumerable<SkillResponse> skillResponses = _mapper.Map<IEnumerable<SkillResponse>>(skills);

            return Ok(skillResponses);
        }

    }
}