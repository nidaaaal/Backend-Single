using Emocare.Application.DTOs.Auth;
using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login(LoginDto dto) {

            var res = await _services.Login(dto);
            var token = res?.Data?.Token;
            if (res.Success && token != null) Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)

            });
            return Ok(res);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(ResponseBuilder.Success("Logged out successfully", "token Removed", "AuthenticationController"));
        }



       [HttpPost("ForgotPassword/ByPrevious")]
        public async Task<IActionResult>ChangeByPrevious (ForgotPasswordDto dto)
            => Ok(await _userService.ForgotPasswordRequest(dto));
       

        [HttpPatch("ForgotPassword/Password")]
        public async Task<IActionResult> ChangePassword(ForgotChangeDto dto)
            => Ok(await _userService.ChangeNewPassword(dto.Email,dto.NewPassword));

        [Authorize]
        [HttpPost("signalRToken")]
        public IActionResult GetToken()
            =>Ok(_services.RefreshSignalRToken());
    }
}
