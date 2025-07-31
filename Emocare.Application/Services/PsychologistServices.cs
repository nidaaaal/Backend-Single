using AutoMapper;
using Emocare.Application.DTOs.User;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Extension;
using Emocare.Domain.Interfaces.Helper.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Shared.Helpers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.Services
{
    public class PsychologistServices:IPsychologistServices
    {
        private readonly IPasswordValidator _passwordValidator;
        private readonly IUserRepository _usersRepository;
        private readonly IPasswordHistoryRepo _passwordHistoryRepo;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public PsychologistServices(IUserRepository userRepository, IPasswordValidator passwordValidator,ICloudinaryService cloudinaryService
            ,IPasswordHasher passwordHasher,IPasswordHistoryRepo passwordHistoryRepo,IMapper mapper
            ) 
        {
            _passwordValidator = passwordValidator;
            _usersRepository = userRepository;
            _cloudinaryService = cloudinaryService;
            _passwordHasher = passwordHasher;
            _passwordHistoryRepo = passwordHistoryRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> PsychologistRegister(PsychologistRegisterDto dto)
        {
            if (dto == null) throw new BadRequestException("UserRegistration Cannot Be Null");

            var existing = await _usersRepository.GetByEmail(dto.EmailAddress.ToLower());
            if (existing != null) return ResponseBuilder.Fail<string>("Email Already Used with Another Account", "UserRegister", 400);

            var (res, message) = _passwordValidator.ValidatePassword(dto.Password);
            if (!res) return ResponseBuilder.Fail<string>(message, "UserRegister", 400);

            if (dto.UploadLicense == null) return ResponseBuilder.Fail<string>("License Copy Need To Upload", "UserRegister", 400);

            string url = await _cloudinaryService.UploadImage(dto.UploadLicense, $"PsychologistLicense/{dto.LicenseNumber}");
            var hashed = _passwordHasher.HashPassword(dto.Password);

            var psychologist = _mapper.Map<Users>(dto);
            psychologist.PasswordHash = hashed;
            psychologist.Role = UserRoles.Psychologist;
            psychologist.PsychologistProfile = new PsychologistProfile
            {
                Id = Guid.NewGuid(),
                UserId = psychologist.Id,
                Specialization = dto.Specialization,
                LicenseNumber = dto.LicenseNumber,
                LicenseCopy = url,
                Education = dto.Education,
                Experience = dto.Experience,
                Biography = dto.Biography,
            };

            await _usersRepository.Add(psychologist);

            var pass = new PasswordHistory
            {
                UserId = psychologist.Id,
                PasswordHash = hashed,
            };
            await _passwordHistoryRepo.Add(pass);

            return ResponseBuilder.Success("Registration Completed", "PsychologistRegister", "UserRegister");
        }
    }
}

