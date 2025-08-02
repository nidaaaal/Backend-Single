
using Emocare.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emocare.Domain.Entities.Habits
{
    public class UserPushSubscription
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string Endpoint { get; set; } = "";

        [Required]
        public string P256dh { get; set; } = "";

        [Required]
        public string Auth { get; set; } = "";

        public DateTime? ExpirationTime { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users? Users { get; set; }
    }
}
