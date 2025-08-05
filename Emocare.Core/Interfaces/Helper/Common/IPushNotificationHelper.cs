
using WebPush;

namespace Emocare.Domain.Interfaces.Helper.Common
{
    public interface IPushNotificationHelper
    {
            Task SendNotificationAsync(PushSubscription subscription, string title, string message);
    }
}
