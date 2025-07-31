using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Task
{
    public class UserDailyTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime DateAssigned { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsRefreshed { get; set; } = false;
    }
}
