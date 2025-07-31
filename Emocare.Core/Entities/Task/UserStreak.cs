using Emocare.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Task
{
    public class UserStreak
    {
            public int Id { get; set; }
            public Guid UserId { get; set; }
            public Users? Users { get; set; }
            public int CurrentStreak { get; set; }
            public DateTime LastUpdated { get; set; }
    }
}
