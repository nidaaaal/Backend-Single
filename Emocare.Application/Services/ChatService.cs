using AutoMapper;
using Emocare.Application.DTOs.Chat;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Shared.Helpers.Api;


namespace Emocare.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatSessionRepository _chatSessionRepo;
        private readonly IChatMessageRepository _messageRepo;
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;
        public ChatService(IChatSessionRepository chatSessionRepository,IChatMessageRepository chatMessageRepository,
            IMapper mapper,IUserFinder userFinder)
        { 
            _chatSessionRepo = chatSessionRepository;
            _messageRepo = chatMessageRepository;
            _userFinder = userFinder;
            _mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<ChatSession?>>> GetChatSessionsAsync() 
        {
            Guid userId = _userFinder.GetId();
            var messages = await _chatSessionRepo.SessionsByUserId(userId) ?? 
            throw new NotFoundException("UserId NotFound, Message Fetching Failed");
            return ResponseBuilder.Success(messages, "Message Fetched Successfully", "GetChatMessagesAsync");
        }

        public async Task<ApiResponse<ChatSession>> GetSessionsAsync(Guid sessionId)
        {
            var session = await _chatSessionRepo.MessageBySessionId(sessionId) ??
            throw new NotFoundException("sessionId NotFound, Message Fetching Failed");
            return ResponseBuilder.Success(session, "Session Fetched Successfully", "GetChatMessagesAsync");
        }

        public async Task<ApiResponse<List<ChatResponseDto>>> GetMessagesAsync()
        {
            Guid userId = _userFinder.GetId();

            var messages = await _messageRepo.MessageByUserId(userId);
            var response = _mapper.Map<List<ChatResponseDto>>(messages);  

            return ResponseBuilder.Success(response, "Message fetched successfully", "GetMessagesAsync");

        }



    }
}
