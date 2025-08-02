using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Infrastructure.Persistence;
using Emocare.Shared.Helpers.Api;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.Chat
{
    public class JournalEntryRepository : Repository<JournalEntry>,IJournalEntryRepository
    {
        public JournalEntryRepository(AppDbContext appContext) : base(appContext) { }

        public async Task<bool> CheckDone(Guid id)
        {
            var find = await _dbSet
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();

            // No journal entry yet → return false (i.e., "not done")
            if (find == null)
                return false;

            return find.Date.Date == DateTime.UtcNow.Date;
        }
        public async Task<JournalEntry> TodayReflection(Guid id)
        {
            return await _dbSet.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.UserId == id) ??
                  throw new NotFoundException("No User found on The corresponding Id");
        } 
        
        public async Task<IEnumerable<JournalEntry>> LastWeek(Guid id)
        {
            var oneweek = DateTime.Now.Date.AddDays(-7);
            return await _dbSet.OrderByDescending(x => x.Date).Where(x => x.UserId == id && x.Date>=oneweek).ToListAsync();
        }

    }
}
