
using System.ComponentModel.DataAnnotations;


namespace Emocare.Application.DTOs.Common
{
    public class RequestSubscription
    {
        public string Endpoint { get; set; } = "";

        [Required]
        public string P256dh { get; set; } = "";

        [Required]
        public string Auth { get; set; } = "";

    }
}
