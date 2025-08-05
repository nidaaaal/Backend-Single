using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Habit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emocare.Domain.Entities.Habits
{
    public class Habit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set;} = string.Empty;

        [Required]
        public int TargetCount { get; set; } = 1;

        [Required]
        public Frequency Frequency { get; set; } = Frequency.Daily;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TimeSpan? ReminderTime { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public  Users? User { get; set; }

        public int CategoryId { get; set; } = 1;
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public  HabitCategory? Category { get; set; }

        public virtual ICollection<HabitCompletion>? Completions { get; set; }

    }
}
