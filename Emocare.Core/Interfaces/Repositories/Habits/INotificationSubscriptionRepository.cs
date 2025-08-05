

using Emocare.Domain.Entities.Habits;

namespace Emocare.Domain.Interfaces.Repositories.Habits
{
    public interface INotificationSubscriptionRepository:IRepository<UserPushSubscription>
    {
        Task<IEnumerable<Habit>> GetTime();
        Task<UserPushSubscription?> IsSubscribed(string endpoint);
    }
}
