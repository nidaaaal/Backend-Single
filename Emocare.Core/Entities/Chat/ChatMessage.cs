using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;

using System.ComponentModel.DataAnnotations;


namespace Emocare.Domain.Entities.Chat
{

    public class ChatMessage
    {
        public int Id { get; set; } 

        public Guid UserId {  get; set; }   

        [Required]
        public UserRoles Role { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; } = string.Empty;

        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid ChatSessionId { get; set; }
        public ChatSession? ChatSession { get; set; } = null!;
    }

}
