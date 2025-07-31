using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.SignalR;
using System.Text;


namespace Emocare.Application.Services
{
    public class ChatHub:Hub
    {
        private readonly IOpenRouterStreamService _streamService;
        private readonly IUserFinder _userFinder;

        public ChatHub(IOpenRouterStreamService streamService,IUserFinder userFinder)
        {
            _streamService = streamService;
            _userFinder = userFinder;
        }
       
        public async Task StartChatSession(string prompt, string mode, string mood)
        {
            Guid userId = _userFinder.GetId();

            var session  = await _streamService.StartNewSession(userId) ?? throw new NotFoundException("UserId NotFound");

            await _streamService.SaveMessageAsync(userId, UserRoles.User, prompt, session.Id);

            var journal = Enum.Parse<JournalMode>(mode);

            StringBuilder reflection = new StringBuilder();

            await foreach (var chunk in _streamService.StreamChatAsync(prompt, journal, mood))
            {
                reflection.Append(chunk);

                await Clients.Caller.SendAsync("ReceiveMessage", chunk);
            }
            string response = reflection.ToString();

            await _streamService.SaveMessageAsync(userId, UserRoles.Ai, response, session.Id);

        }
    }
}
