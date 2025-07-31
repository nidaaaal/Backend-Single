using Emocare.Domain.Enums.AiChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.DTOs.Task
{
    public class InsertTaskDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public string Category { get; set; } = "General";
        public Mood MoodTags { get; set; }
    }
}
