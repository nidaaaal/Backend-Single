using AutoMapper;
using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Extension;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Helper.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.Http;

namespace Emocare.Application.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IPasswordHistoryRepo _historyRepo;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;
        private readonly IUserFinder _userFinder;
        public UserServices(IUserRepository userRepository, IPasswordValidator passwordValidator,IUserFinder userFinder,
            IPasswordHasher passwordHasher, IPasswordHistoryRepo passwordHistoryRepo, IMapper mapper,ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _historyRepo = passwordHistoryRepo;
            _mapper = mapper;
            _cloudinary = cloudinaryService;    
            _userFinder = userFinder;
        }

        public async Task<ApiResponse<string>> UserRegister(UserRegisterDto dto)
        {
            var existing = await _userRepository.GetByEmail(dto.EmailAddress.ToLower());
            if (existing != null) return ResponseBuilder.Fail<string>("Email Already Used with Another Account", "UserRegister", 400);
            var (res, message) = _passwordValidator.ValidatePassword(dto.Password);
            if (!res) return ResponseBuilder.Fail<string>(message, "UserRegister", 400);
            var hashed = _passwordHasher.HashPassword(dto.Password);
            var user = _mapper.Map<Users>(dto);
            user.PasswordHash = hashed;
            await _userRepository.Add(user);
            var pass = new PasswordHistory
            {
                UserId = user.Id,
                PasswordHash = hashed,
            };
            await _historyRepo.Add(pass);

            return ResponseBuilder.Success($"{dto.FullName} Registered Successfully", "Registration Completed", "UserRegister");
        }
        public async Task<ApiResponse<string>> ChangePassword(PasswordChangeDto dto)
        {
            Guid userId = _userFinder.GetId();
            var user = await _userRepository.GetByEmail(dto.Email);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "ChangePassword", 404);
            if (user.Id != userId) return ResponseBuilder.Fail<string>("Invalid Email Id.Try Again! ", "ChangePassword", 404);
            if (!_passwordHasher.VerifyPassword(dto.OldPassword, user.PasswordHash)) return ResponseBuilder.Fail<string>("Incorrect Password", "ChangePassword", 404);
            if (_passwordHasher.VerifyPassword(dto.NewPassword, user.PasswordHash)) return ResponseBuilder.Fail<string>("cannot change with older Password", "ChangePassword", 404);
            var (res, mes) = _passwordValidator.ValidatePassword(dto.NewPassword);
            if (!res) return ResponseBuilder.Fail<string>(mes, "ChangePassword", 404);
            var hashed = _passwordHasher.HashPassword(dto.NewPassword);
            user.PasswordHash = hashed;
            user.PasswordChangedAt = DateTime.UtcNow;
            var history = new PasswordHistory
            {
                UserId = user.Id,
                PasswordHash = hashed,
            };
            await _historyRepo.Add(history);


            await _userRepository.Update(user);
            return ResponseBuilder.Success("Password Changed successfully", "User Data updated", "ChangePassword");

        }
        public async Task<ApiResponse<string>> ForgotPasswordRequest(ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "ChangePassword", 404);
            var password = await _historyRepo.PreviousPassword(user.Id) ?? throw new BadRequestException("Invalid userId");
            if (!_passwordHasher.VerifyPassword(dto.PreviousPassword, password.PasswordHash))
            {
                user.ChangeAttempt += 1;
                while (user.ChangeAttempt > 6)
                {
                    user.Status = UserStatus.Inactive;
                    return ResponseBuilder.Fail<string>("Your account has been locked due to multiple suspicious change attempts. Please contact support."
                        , "ForgotPassword", 423);
                }
                return ResponseBuilder.Fail<string>("Incorrect Previous Password", "ForgotPassword", 500);
            }
            user.ChangeRequest = true;
            await _userRepository.Update(user);
            return ResponseBuilder.Success("Request Accepted", "User Data Updated");
        }
        public async Task<ApiResponse<string>> ChangeNewPassword(string email,string password)
        {
            Guid userId = _userFinder.GetId();

            var user = await _userRepository.GetByEmail(email);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "ChangePassword", 404);
            if (user.ChangeRequest)
            {
                if (_passwordHasher.VerifyPassword(password, user.PasswordHash)) return ResponseBuilder.Fail<string>("cannot change with older Password", "ChangePassword", 500);
                var previous = await _historyRepo.PreviousPassword(user.Id) ?? throw new BadRequestException("Invalid userId");
                if (!_passwordHasher.VerifyPassword(password, previous.PasswordHash)) return ResponseBuilder.Fail<string>("cannot change with older Password", "ChangePassword", 500);


                var (res, mes) = _passwordValidator.ValidatePassword(password);
                if (!res) return ResponseBuilder.Fail<string>(mes, "ChangePassword", 404);
                var hashed = _passwordHasher.HashPassword(password);
                user.PasswordHash = hashed;
                user.PasswordChangedAt = DateTime.Now;
                var history = new PasswordHistory
                {
                    UserId = user.Id,
                    PasswordHash = hashed,
                };
                await _historyRepo.Add(history);
                await _userRepository.Update(user);


                return ResponseBuilder.Success("Password Changed successfully", "User Data updated", "ChangePassword");
            }
            return ResponseBuilder.Fail<string>("No Access to change password.Try Again!", "ChangePassword", 404);
        }

        public async Task<ApiResponse<UserProfileDto>?> ViewProfile()
        {
            Guid id =  _userFinder.GetId();

            var user = await _userRepository.GetById(id) ?? throw new NotFoundException("Invalid UserId");

            var profile = _mapper.Map<UserProfileDto>(user);

            return ResponseBuilder.Success(profile, "Profile Fetched Successfully", "ViewProfile");
        }

        public async Task<ApiResponse<UserProfileDto>?> UpdateProfile(UpdateProfileDto dto)
        {
            Guid id = _userFinder.GetId();

            var user = await _userRepository.GetById(id) ?? throw new NotFoundException("Invalid UserId");

            user.FullName = dto.FullName;
            user.Age = dto.Age;
            user.Gender = dto.Gender;
            user.RelationshipStatus = dto.RelationshipStatus;
            user.Job = dto.Job;
            user.City = dto.City;
            user.Country = dto.Country;

            var profile = _mapper.Map<UserProfileDto>(user);
            await _userRepository.Update(user);

            return ResponseBuilder.Success(profile, "Profile Updated Successfully", "UpdateProfile");
        }

        public async Task<ApiResponse<string>?> UpdateProfilePicture(IFormFile ImageFile)
        {
           if(ImageFile==null) throw new BadRequestException("No Image Found ");

            Guid id = _userFinder.GetId();

            var user = await  _userRepository.GetById(id) ?? throw new NotFoundException("Invalid UserId");
            if (user.ProfileImageUrl != null) 
            {
                 if (await _cloudinary.DeleteImageAsync(user.ProfileImageUrl))
                {
                    string url = await _cloudinary.UploadImage(ImageFile, $"ProfilePicture/${id}");
                    user.ProfileImageUrl = url;
                   await _userRepository.Update(user);

                   return ResponseBuilder.Success("New ProfilePicture Added","ProfilePicture Updated Successfully", "UpdateProfile");
                }

                return ResponseBuilder.Fail<string>("Failed To Add New ProfilePicture", "UpdateProfile");
            }

            string img = await _cloudinary.UploadImage(ImageFile, $"ProfilePicture/${id}");
            user.ProfileImageUrl = img;
            await _userRepository.Update(user);

            return ResponseBuilder.Success("New ProfilePicture Added", "ProfilePicture Added Successfully", "UpdateProfile");
        }
        public async Task<ApiResponse<IEnumerable<Users>>> GetAllDetails()
        {
            var users = await _userRepository.GetAllActive();
            return ResponseBuilder.Success(users, "Users Data Fetched", "GetAllDetails");

        }

        public async Task<ApiResponse<string>> BanUser(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return ResponseBuilder.Fail<string>("No UserFound", "BanUser", 404);
            user.Status = UserStatus.Banned;
            await _userRepository.Update(user);
            return ResponseBuilder.Success("User Banned", "Users Data Updated", "BanUser");

        }
    }
}
