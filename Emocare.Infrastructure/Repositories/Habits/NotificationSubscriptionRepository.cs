using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.Habits
{
    public class NotificationSubscriptionRepository:Repository<UserPushSubscription>,INotificationSubscriptionRepository
    {
        public NotificationSubscriptionRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<Habit>> GetTime()
        {
            var now = DateTime.UtcNow;
            var habitsDue = await _context.Habits
                .Include(h => h.User)
                .ThenInclude(u => u.UserPushSubscriptions)
                .Where(h => h.ReminderTime.HasValue &&
                           h.ReminderTime.Value.Hours == now.Hour &&
                           h.ReminderTime.Value.Minutes == now.Minute)
                .ToListAsync();

            return habitsDue;
        }
        public async Task<UserPushSubscription?> IsSubscribed(string endpoint)
            =>await _context.UserPushSubscriptions.SingleOrDefaultAsync(s => s.Endpoint == endpoint);
    }
}
