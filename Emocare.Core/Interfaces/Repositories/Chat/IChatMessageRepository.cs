using Emocare.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories.Chat
{
    public interface IChatMessageRepository : IRepository<ChatMessage>
       
    {
        Task<IEnumerable<ChatMessage>> MessageByUserId(Guid id);
    }
}
