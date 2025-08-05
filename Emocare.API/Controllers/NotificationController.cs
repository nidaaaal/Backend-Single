using Emocare.Application.DTOs.Common;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationsServices _notificationsServices;

        public NotificationController(INotificationsServices notificationServices)
        {
            _notificationsServices = notificationServices;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(RequestSubscription requestSubscription)
            => Ok(await _notificationsServices.SubscribeNotification(requestSubscription));

        [HttpPost("reminder")]
        public async Task<IActionResult> Reminder(RequestSubscription requestSubscription)
            => Ok(await _notificationsServices.SendDueHabitReminders());

    }
}
