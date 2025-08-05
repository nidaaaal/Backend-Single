
using AutoMapper;
using Emocare.Application.DTOs.Common;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Helper.Common;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Shared.Helpers.Api;
using WebPush;

namespace Emocare.Application.Services
{
    public class NotificationsServices: INotificationsServices
    {
        private readonly IPushNotificationHelper  _pushServices;
        private readonly INotificationSubscriptionRepository _notificationSubscriptionRepository;
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;
        public NotificationsServices(IPushNotificationHelper pushNotification,IUserFinder userFinder,
            INotificationSubscriptionRepository notificationSubscriptionRepository,IMapper mapper) 
        {
            _pushServices = pushNotification;
            _notificationSubscriptionRepository = notificationSubscriptionRepository;   
            _userFinder = userFinder;
            _mapper = mapper;
        
        }

        public async Task<ApiResponse<string>> SubscribeNotification(RequestSubscription request)
        {
            Guid userId = _userFinder.GetId();
           var res = await _notificationSubscriptionRepository.IsSubscribed(request.Endpoint);
            if (res != null)
            {
                res.P256dh = request.P256dh;
                res.Auth = request.Auth;
                await _notificationSubscriptionRepository.Update(res);
            }
            else
            {
                var subscription = _mapper.Map<UserPushSubscription>(request);
                subscription.UserId = userId;

                await _notificationSubscriptionRepository.Add(subscription);
            }

          return ResponseBuilder.Success("Subscribed", "subscription Added", "NotificationsServices");   
        }
        public async Task<ApiResponse<string>> SendDueHabitReminders()
        {
            var habitsDue = await _notificationSubscriptionRepository.GetTime();
            int sentCount = 0;
            foreach (var habit in habitsDue)
            {
                foreach(var subscription in habit.User.UserPushSubscriptions)
                {
                    var pushSubscription = new PushSubscription
                    {
                        Endpoint = subscription.Endpoint,
                        P256DH = subscription.P256dh,
                        Auth = subscription.Auth
                    };

                    await _pushServices.SendNotificationAsync(
                    pushSubscription,
                    $"Time to {habit.Name}",
                    $"Target: {habit.TargetCount} {habit.Frequency}"
                    );
                    sentCount++;

                }
            }
            return ResponseBuilder.Success("Reminders Sent", $"Sent {sentCount} notifications", "NotificationService");

        }


    }
}
