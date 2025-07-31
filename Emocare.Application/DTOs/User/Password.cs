using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Emocare.Application.DTOs.User
{
    #region request
    public class ForgotPasswordDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = "";

        public string PreviousPassword { get; set; } = "";
    }
    public class PasswordChangeDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

    }

    public class ForgotChangeDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

    }

    #endregion


    #region Response

    public class PasswordHistoryDto
    {

        public Guid UserId { get; set; }

        public string PasswordHash { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? LastUpdated { get; set; }


    } 
    #endregion
}
