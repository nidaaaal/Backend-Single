using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Helper.Auth
{
    public interface IPasswordValidator
    {
        (bool isValid, string message) ValidatePassword(string password);

    }
}
