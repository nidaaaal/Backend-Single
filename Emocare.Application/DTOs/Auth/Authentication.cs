using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.DTOs.Auth
{
    #region request
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = "";
    }
    #endregion

    #region response
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "";
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
        public bool IsPsychologist { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public string Token { get; set; } = "";
        public string SignalRToken { get; set; } = "";

        public string RefreshToken { get; set; } = "";
    } 
    #endregion

}
