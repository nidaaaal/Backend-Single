using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Emocare.Domain.Entities.Habits
{
   
    public class HabitCompletion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HabitId { get; set; }

        [ForeignKey("HabitId")]
        public Habit? Habit { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        public int Count { get; set; } = 1;

        public string Notes { get; set; } = string.Empty;
    }
}
