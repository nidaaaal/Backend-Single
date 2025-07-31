using Emocare.Application.DTOs.Habits;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Shared.Helpers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.Services
{
    public class HabitServices:IHabitServices
    {
        private readonly IHabitCategoryRepository _habitCategoryRepository;
        private readonly IHabitRepository _habitRepository;
        private readonly IHabitHabitCompletionRepository _completionRepository;

        public HabitServices(IHabitHabitCompletionRepository completionRepository,IHabitCategoryRepository habitCategoryRepository,
            IHabitRepository habitRepository) 
        {
            _completionRepository = completionRepository;
            _habitCategoryRepository = habitCategoryRepository;
            _habitRepository = habitRepository;
        }

        public async Task<ApiResponse<string>> CreateHabitAsync(AddHabit dto)
        {
            _
        }
        public async Task<ApiResponse<string>?> UpdateHabitAsync(int id, AddHabit dto)
        {

        }
        public async Task<ApiResponse<string>?> DeleteHabitAsync(int id)
        {

        }
        public async Task<ApiResponse<Habit?>> GetHabitAsync(int id)
        {

        }
        public async Task<ApiResponse<IEnumerable<Habit?>>> GetUserHabitsAsync(Guid userId)
        {

        }
        public async Task<ApiResponse<string>> RecordCompletionAsync(int habitId, DateTime date, int count = 1, string notes = null)
        {

        }
        public async Task<ApiResponse<IEnumerable<HabitCompletion>>> GetCompletionsAsync(int habitId, DateTime? startDate, DateTime? endDate)
        {

        }
        public async Task<ApiResponse<HabitStats>> GetHabitStatsAsync(int habitId)
        {

        }

    }
}
