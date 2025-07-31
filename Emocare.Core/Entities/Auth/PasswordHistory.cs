using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Auth
{
    public class PasswordHistory
    {
        public int Id { get; set; }     

        public Guid UserId { get; set; }
        public Users? Users { get; set; }

        public string PasswordHash { get; set; } = "";  

        public DateTime Created { get; set; }= DateTime.Now;


    }
}
