using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Emocare.Infrastructure.Repositories.Chat
{
    public class ChatMessageRepository:Repository<ChatMessage>,IChatMessageRepository
    {
        public ChatMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<ChatMessage>> MessageByUserId(Guid id)=> await _dbSet.OrderBy(x=>x.SentAt).Where(x=>x.UserId== id).ToListAsync();
    }
}
