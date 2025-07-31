using Emocare.Application.DTOs.User;
using Emocare.Shared.Helpers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.Interfaces
{
    public interface IPsychologistServices
    {
        Task<ApiResponse<string>> PsychologistRegister(PsychologistRegisterDto dto);

    }
}
