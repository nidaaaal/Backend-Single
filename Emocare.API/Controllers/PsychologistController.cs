using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Emocare.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/psychologist/")]
    [ApiController]
    public class PsychologistController : ControllerBase
    {
        private readonly IPsychologistServices _psychologistServices;
        public PsychologistController(IPsychologistServices psychologistServices)
        {
            _psychologistServices = psychologistServices;
        }

        [HttpGet("allPsychologist")]
        public async Task<IActionResult> GetPsychologist()
           => Ok(await _psychologistServices.GetAllPsychologist());


        [HttpPatch("psychologist/{id}")]
        public async Task<IActionResult> Verify(Guid id)
            => Ok(await _psychologistServices.VerifyPsychologist(id));

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPsychologist([FromForm] PsychologistRegisterDto dto)
            =>  Ok(await _psychologistServices.PsychologistRegister(dto));
     }
}
