using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.AiChat;

namespace Emocare.Domain.Entities.Journal
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JournalEntry
    {
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Users User { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public Mood Mood { get; set; } = Mood.Unknown;

        [Required]
        public JournalMode Mode { get; set; } = JournalMode.Journal;

        [Required(ErrorMessage = "Journal entry text is required.")]
        [StringLength(1000, ErrorMessage = "Journal entry cannot exceed 2000 characters.")]
        public string Entry { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "AI reflection cannot exceed 2000 characters.")]
        public string AIReflection { get; set; } = string.Empty;
    }


}
