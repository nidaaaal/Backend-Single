using Azure;
using Emocare.Application.DTOs.Auth;
using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _services;
        private readonly IUserService _userService; 
        public AuthenticationController(IAuthenticationServices services, IUserService userService  )
        {
            _services = services;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
            => Ok(await _services.Login(dto));


        [HttpPost("ForgotPassword/ByPrevious")]
        public async Task<IActionResult>ChangeByPrevious (ForgotPasswordDto dto)
            => Ok(await _userService.ForgotPasswordRequest(dto));
       

        [HttpPatch("ForgotPassword/Password")]
        public async Task<IActionResult> ChangePassword(ForgotChangeDto dto)
            => Ok(await _userService.ChangeNewPassword(dto.Email,dto.NewPassword));
       
    }
}
