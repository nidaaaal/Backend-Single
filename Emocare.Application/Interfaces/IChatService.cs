

using Emocare.Application.DTOs.Chat;
using Emocare.Domain.Entities.Chat;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IChatService
    {
        Task<ApiResponse<IEnumerable<ChatSession?>>> GetChatSessionsAsync();
        Task<ApiResponse<ChatSession>> GetSessionsAsync(Guid sessionId);
        Task<ApiResponse<List<ChatResponseDto>>> GetMessagesAsync();


    }
}
