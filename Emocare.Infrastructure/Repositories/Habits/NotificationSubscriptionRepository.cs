using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Infrastructure.Persistence;

namespace Emocare.Infrastructure.Repositories.Habits
{
    public class NotificationSubscriptionRepository:Repository<UserPushSubscription>,INotificationSubscriptionRepository
    {
        public NotificationSubscriptionRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
