using Emocare.Domain.Entities.Journal;
using Emocare.Shared.Helpers.Api;


namespace Emocare.Application.Interfaces
{
    public interface IReflectionServices
    {
        Task<ApiResponse<string>> GetReflection(string prompt, string mood);
        Task<ApiResponse<string>?> DailyReflection();
        Task<ApiResponse<IEnumerable<JournalEntry?>>> LastWeekDailyReflection();
    }
}
