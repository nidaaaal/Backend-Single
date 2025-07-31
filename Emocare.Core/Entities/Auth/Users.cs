using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Enums.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Auth
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [Required,MinLength(3),MaxLength(30)]
        public string FullName { get; set; } = string.Empty;

        [Required,Range(13,99)]
        public int? Age { get; set; }

        [Required,MaxLength(6)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Job {  get; set; } = string.Empty;

        [Required]
        public string RelationshipStatus { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set;} = string.Empty;

        public int FailedLoginAttempts { get; set; } = 0;

        public int ChangeAttempt { get; set; } = 0; 

        public bool ChangeRequest { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        public DateTime? LastUpdate { get; set; }

        public UserRoles? UpdatedBy { get; set; }

        public DateTime? PasswordChangedAt { get; set; }

        [Required]
        public UserRoles Role { get; set; } = UserRoles.User;

        [Required]
        public UserStatus Status { get; set; } = UserStatus.Active;

        [Url]
        public string? ProfileImageUrl { get; set; }
        public bool IsAdmin => Role == UserRoles.Admin;
        public bool IsUser => Role == UserRoles.User;
        public bool IsPsychologist => Role == UserRoles.Psychologist;

        public bool IsActive => Status == UserStatus.Active;
        public bool IsLocked => Status == UserStatus.Locked;
        public bool IsBanned => Status == UserStatus.Banned;

        public PsychologistProfile? PsychologistProfile {  get; set; }
        public ICollection<PasswordHistory>? PasswordHistory { get; set; }
        public ICollection<JournalEntry>? JournalEntries { get; set; } 
        public ICollection<ChatSession>? ChatSessions { get; set; }
        public ICollection<ChatMessage>? ChatMessages { get; set; }
    }

}