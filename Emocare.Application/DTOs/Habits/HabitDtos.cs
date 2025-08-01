
using Emocare.Domain.Enums.Habit;

using System.ComponentModel.DataAnnotations;


namespace Emocare.Application.DTOs.Habits
{
    #region Response
    public class HabitStats
    {
        public int TotalCompletions { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public decimal CompletionPercentage { get; set; }
    }
    #endregion

    #region Request
    public class AddHabit
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TargetCount { get; set; } = 1;

        [Required]
        public Frequency Frequency { get; set; } = Frequency.Daily;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int CategoryId { get; set; }

    }

    public class CompletionRequest
    {
        public DateTime? Date { get; set; }
        public int? Count { get; set; }
        public string Notes { get; set; }
    }
    #endregion
}
