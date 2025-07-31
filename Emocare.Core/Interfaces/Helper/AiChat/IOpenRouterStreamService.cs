using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Auth;

namespace Emocare.Domain.Interfaces.Helper.AiChat
{
    public interface IOpenRouterStreamService
    {
        IAsyncEnumerable<string> StreamChatAsync(string prompt, JournalMode mode,string mood);
        Task<ChatSession> StartNewSession(Guid userId);
        Task SaveMessageAsync(Guid userId, UserRoles sender, string message, Guid sessionId);
    }
}
