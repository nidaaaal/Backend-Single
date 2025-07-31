using Emocare.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Chat
{
      public class ChatSession
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            [Required]
            public Guid UserId { get; set; }
            public Users User { get; set; }

            [Required]
            public DateTime StartedAt { get; set; }

            public ICollection<ChatMessage> Messages { get; set; }
        }

}
