

using Emocare.Application.DTOs.Habits;
using Emocare.Domain.Entities.Habits;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IHabitServices
    {
        Task <ApiResponse<string>> CreateHabitAsync(AddHabit dto);
        Task <ApiResponse<string>?> UpdateHabitAsync(int id,AddHabit dto);
        Task <ApiResponse<string>?> DeleteHabitAsync(int id);
        Task <ApiResponse<Habit>?> GetHabitAsync(int id);
        Task <ApiResponse<IEnumerable<Habit?>>> GetUserHabitsAsync(Guid userId);
        Task<ApiResponse<string>?>RecordCompletionAsync(int habitId, DateTime date, string notes, int count = 1 );
        Task<ApiResponse<IEnumerable<HabitCompletion?>>> GetCompletionsAsync(int habitId, DateTime startDate, DateTime endDate);
        Task <ApiResponse<HabitStats>?> GetHabitStatsAsync(int habitId);

    }
}
