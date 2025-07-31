using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Enums.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.DTOs.Chat
{
    public class ChatResponseDto
    {
        public int Id { get; set; }

        public string Role { get; set; } = "";

        public string Message { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }
    }
}
