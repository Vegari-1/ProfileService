using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Dto;
using ProfileService.Service.Interface;

namespace ProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileSerivce, IMapper mapper)
        {
            _profileService = profileSerivce;
            _mapper = mapper;
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