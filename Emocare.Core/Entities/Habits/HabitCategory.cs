using Emocare.Domain.Entities.Habits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Habits
{
    public class HabitCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public ICollection<Habit>? Habits { get; set; }


        //    new HabitCategory { Id = 1, Name = "Health & Fitness", ColorCode = "#4CAF50" },
        //    new HabitCategory { Id = 2, Name = "Productivity", ColorCode = "#2196F3" },
        //    new HabitCategory { Id = 3, Name = "Mindfulness", ColorCode = "#9C27B0" },
        //    new HabitCategory { Id = 4, Name = "Learning", ColorCode = "#FF9800" },
        //    new HabitCategory { Id = 5, Name = "Relationships", ColorCode = "#E91E63" }
    }
}
