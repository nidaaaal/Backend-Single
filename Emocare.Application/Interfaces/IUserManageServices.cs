using Emocare.Domain.Entities.Auth;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IUserManageServices
    {
        Task<ApiResponse<IEnumerable<Users>>> GetAllDetails();
        Task<ApiResponse<IEnumerable<PsychologistProfile>>> GetAllPsychologist();
        Task<ApiResponse<string>> VerifyPsychologist(Guid id);
        Task<ApiResponse<string>> BanUser(Guid userId);
        
    }
}
