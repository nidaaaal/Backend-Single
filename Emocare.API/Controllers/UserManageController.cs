using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Authorize(Policy ="AdminOnly")]
    [Route("api/admin")]
    [ApiController]
    public class UserManageController : ControllerBase
    {
        private readonly IUserManageServices _userServices;

        public UserManageController(IUserManageServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("allUsers")]
        public async Task<IActionResult> GetAll()
            => Ok(await _userServices.GetAllDetails());


        [HttpGet("allPsychologist")]
        public async Task<IActionResult> GetPsychologist()
            => Ok(await _userServices.GetAllPsychologist());


        [HttpPatch("psychologist/{id}")]
        public async Task<IActionResult> Verify(Guid id)
            => Ok( await _userServices.VerifyPsychologist(id));


        [HttpPatch("user/{id}")]
        public async Task<IActionResult> Ban(Guid id) 
            => Ok(await _userServices.BanUser(id));

    }
}
