using Emocare.Application.DTOs.Auth;
using Emocare.Application.Interfaces;
using Emocare.Shared.Helpers.Api;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Helper.Auth;
using Emocare.Domain.Interfaces.Repositories.User;

namespace Emocare.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtHelper _jwtHelper;
        public AuthenticationServices(IUserRepository userRepository,IPasswordHasher passwordHasher, IJwtHelper jwtHelper)
        { 

            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }
        public async Task<ApiResponse<AuthResponse>> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email.ToLower());
            if (user == null) return ResponseBuilder.Fail<AuthResponse>("User Not Found", "Login", 404);
            if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            {
                user.FailedLoginAttempts += 1;
                await _userRepository.Update(user);
                if (user.FailedLoginAttempts > 9)
                {
                    user.Status = UserStatus.Locked;
                    await _userRepository.Update(user);
                    return ResponseBuilder.Fail<AuthResponse>(
                        "Your account has been locked due to multiple suspicious login attempts. Please contact support.",
                        "Login",
                        423);
                }
                return ResponseBuilder.Fail<AuthResponse>("Invalid Password ! Try Again", "Login", 401);
            }
            user.FailedLoginAttempts = 0;
            user.LastLogin = DateTime.UtcNow;
            await _userRepository.Update(user);
            string token = _jwtHelper.GetJwtToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                IsUser = user.IsUser,
                IsPsychologist = user.IsPsychologist,
                IsLocked = user.IsLocked,
                Role = user.Role.ToString(),
                Token = token
            };

            return ResponseBuilder.Success(response, "Login Successful", "Login");
        }

    }
}
