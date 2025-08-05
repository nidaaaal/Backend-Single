

using Emocare.Application.DTOs.Common;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface INotificationsServices
    {
        Task<ApiResponse<string>> SubscribeNotification(RequestSubscription request);
        Task<ApiResponse<string>> SendDueHabitReminders();
    }
}
