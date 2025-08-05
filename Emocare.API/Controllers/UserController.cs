using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;

        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
           => Ok(await _userServices.GetAllDetails());

        [HttpPatch("{id}")]
        public async Task<IActionResult> Ban(Guid id)
       => Ok(await _userServices.BanUser(id));

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto dto)
            => Ok(await _userServices.UserRegister(dto));


        [Authorize]
        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword(PasswordChangeDto dto)
            => Ok(await _userServices.ChangePassword(dto));
     

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> ViewProfile()
            => Ok(await _userServices.ViewProfile());


        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
            => Ok(await _userServices.UpdateProfile(dto));


        [Authorize]
        [HttpPatch("profilePicture")]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile file)
            => Ok(await _userServices.UpdateProfilePicture(file));
         
    }
}
