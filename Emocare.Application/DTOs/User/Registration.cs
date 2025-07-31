using Emocare.Application.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.DTOs.User
{
    #region Register
    public class PsychologistRegisterDto
    {
        [Required, MinLength(3), MaxLength(30)]
        public string FullName { get; set; } = string.Empty;

        [Required, Range(13, 99)]
        public int Age { get; set; }

        [Required, MaxLength(6)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Job { get; set; } = string.Empty;

        [Required]
        public string RelationshipStatus { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = null!;

        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = "";
        [Required, MaxLength(100)]
        public string Specialization { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LicenseNumber { get; set; } = null!;

        [NotMapped]
        public IFormFile? UploadLicense { get; set; }

        [Range(0, 60)]
        public int? Experience { get; set; }

        [MaxLength(1000)]
        public string? Biography { get; set; }

        [MaxLength(200)]
        public string? Education { get; set; }

    }
    public class UserRegisterDto
    {

        [Required, MinLength(3), MaxLength(30)]
        public string FullName { get; set; } = string.Empty;

        [Required, Range(13, 99)]
        public int Age { get; set; }

        [Required, MaxLength(6)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Job { get; set; } = string.Empty;

        [Required]
        public string RelationshipStatus { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = null!;

        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = "";

    } 
    #endregion
}
