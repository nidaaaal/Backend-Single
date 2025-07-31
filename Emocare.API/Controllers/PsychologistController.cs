using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
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


        [HttpPost("register")]
        public async Task<IActionResult> RegisterPsychologist([FromForm] PsychologistRegisterDto dto)
            =>  Ok(await _psychologistServices.PsychologistRegister(dto));
     }
}
