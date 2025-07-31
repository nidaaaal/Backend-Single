using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("sessions")]
        public async Task<IActionResult> GetSession() 
            => Ok (await _chatService.GetChatSessionsAsync());

        [HttpGet("message")]
        public async Task<IActionResult> GetMessage()
           => Ok(await _chatService.GetMessagesAsync());


        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetSession(Guid sessionId) 
            => Ok(await _chatService.GetSessionsAsync(sessionId));

    }
}
