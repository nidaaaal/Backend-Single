using AutoMapper;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Shared.Helpers.Api;


namespace Emocare.Application.Services
{
    public class UserManageServices: IUserManageServices
    {
        private readonly IUserRepository _userRepo;
        private readonly IPsychologistRepository _psychologistRepo;
        public UserManageServices(IUserRepository userRepository,IPsychologistRepository psychologistRepo)
        {
            _userRepo = userRepository; 
            _psychologistRepo = psychologistRepo;   
        }
       public async Task<ApiResponse<IEnumerable<Users>>> GetAllDetails()
        {
            var users = await _userRepo.GetAllActive();
            return ResponseBuilder.Success(users,"Users Data Fetched", "GetAllDetails");

        }
       public async Task<ApiResponse<IEnumerable<PsychologistProfile>>> GetAllPsychologist()
        {
            var users = await _psychologistRepo.GetAll();
            return ResponseBuilder.Success(users, "Users Data Fetched", "GetAllPsychologist");
        }
       public async Task<ApiResponse<string>> VerifyPsychologist(Guid id)
        {
            var user = await _psychologistRepo.GetById(id);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "VerifyPsychologist", 404); 
            if(user.IsApproved) return ResponseBuilder.Fail<string>("User Already Verified", "VerifyPsychologist", 409);
            user.IsApproved = true;
            user.ApprovedOn = DateTime.UtcNow;
            await _psychologistRepo.Update(user);
            return ResponseBuilder.Success("Psychologist Approved", "Users Data Updated", "VerifyPsychologist");

        }
        public async Task<ApiResponse<string>> BanUser(Guid userId)
        {
            var user = await _userRepo.GetById(userId);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "BanUser", 404);
            user.Status=UserStatus.Banned;
            await _userRepo.Update(user);
            return ResponseBuilder.Success("User Banned", "Users Data Updated", "BanUser");

        }
    }
}
