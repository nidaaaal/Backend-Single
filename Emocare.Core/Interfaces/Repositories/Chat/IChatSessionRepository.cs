using Emocare.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories.Chat
{
    public interface IChatSessionRepository : IRepository<ChatSession>
    {
        Task<ChatSession?> MessageBySessionId(Guid id);
        Task<IEnumerable<ChatSession?>> SessionsByUserId(Guid id);
        Task<ChatSession?> SessionExist(Guid id);
    }
}
