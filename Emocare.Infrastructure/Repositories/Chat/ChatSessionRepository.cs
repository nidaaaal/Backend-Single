using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Repositories.Chat
{
    public class ChatSessionRepository : Repository<ChatSession>, IChatSessionRepository
    {
        public ChatSessionRepository(AppDbContext dbContext) : base(dbContext) { }
        public async Task<IEnumerable<ChatSession?>> SessionsByUserId(Guid id) => await _dbSet.Include(x => x.Messages).Where(x => x.Id == id).ToListAsync();

        public async Task<ChatSession?> MessageBySessionId(Guid id) => await _dbSet.Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == id);
        public async  Task<ChatSession?> SessionExist(Guid id)=> await _dbSet.Include(x=>x.Messages).FirstOrDefaultAsync(x=>x.UserId == id && x.StartedAt.Date == DateTime.Now.Date);
    }
}