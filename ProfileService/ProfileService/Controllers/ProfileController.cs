using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using ProfileService.Dto;
using ProfileService.Service.Interface;
using Prometheus;

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
        [Route("/{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            Model.Profile profile = await _profileService.GetById(id);
            
            ProfileResponse profileResponse = _mapper.Map<ProfileResponse>(profile);

            return Ok(profileResponse);
        }

    }
}