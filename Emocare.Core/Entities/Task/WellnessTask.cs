using Emocare.Domain.Enums.AiChat;

namespace Emocare.Domain.Entities.Task
{
    public class WellnessTask
    {
            public int Id { get; set; }
            public string Title { get; set; } = "";
            public string? Description { get; set; }
            public string Category { get; set; } = "General";
            public Mood MoodTags { get; set; }
            public DateTime AddedOn { get; set; }
            public bool IsActive { get; set; } = true;
    }
}