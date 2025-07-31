

using Emocare.Application.DTOs.Auth;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IAuthenticationServices
    {
        Task <ApiResponse<AuthResponse>> Login(LoginDto dto);
    }
}
