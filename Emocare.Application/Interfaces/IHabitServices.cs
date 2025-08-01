﻿

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
        Task <ApiResponse<IEnumerable<Habit?>>> GetUserHabitsAsync();
        Task<ApiResponse<string>?>RecordCompletionAsync(int habitId, CompletionRequest completion);
        Task<ApiResponse<IEnumerable<HabitCompletion?>>> GetCompletionsAsync(int habitId);
        Task <ApiResponse<HabitStats>?> GetHabitStatsAsync(int habitId);

    }
}
