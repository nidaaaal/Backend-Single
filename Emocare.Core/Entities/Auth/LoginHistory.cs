using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Auth
{
    public class LoginHistory
    {

        public string IpAddress { get; set; } ="";
        public string Model { get; set; } = "";
        public string StartedAt { get; set; } = "";
        public string DeviceType { get; set; } = "";
    }
}
