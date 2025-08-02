using AutoMapper;
using Emocare.Application.DTOs.Habits;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Services
{
    public class HabitServices:IHabitServices
    {
        private readonly IHabitCategoryRepository _habitCategoryRepository;
        private readonly IHabitRepository _habitRepository;
        private readonly IHabitHabitCompletionRepository _completionRepository;
        private readonly IUserFinder _userFind;
        private readonly IMapper _mapper;
        public HabitServices(IHabitHabitCompletionRepository completionRepository,IHabitCategoryRepository habitCategoryRepository,
            IHabitRepository habitRepository,IUserFinder userFinder,IMapper mapper) 
        {
            _completionRepository = completionRepository;
            _habitCategoryRepository = habitCategoryRepository;
            _habitRepository = habitRepository;
            _userFind = userFinder;
            _mapper = mapper;   
        }

        public async Task<ApiResponse<string>> CreateHabitAsync(AddHabit dto)
        {
            Guid id = _userFind.GetId();
            Habit habit = _mapper.Map<Habit>(dto);  
            habit.UserId=id;
            await _habitRepository.Add(habit);
            return ResponseBuilder.Success("New Habit Added ","New Habit Successfully", "HabitServices");
            
        }
        public async Task<ApiResponse<string>?> UpdateHabitAsync(int id, AddHabit dto)
        {
            var userId = _userFind.GetId();

            var exist = await _habitRepository.GetById(id) ?? throw new NotFoundException("Habit Id Not Found!");
            if (exist.UserId != userId) throw new ForbiddenException("No Access to this Id ! Id miss match");
            var updated = _mapper.Map(dto, exist) ?? throw new BadRequestException("mapping failed");
            bool res = await _habitRepository.Update(updated);
            if (!res) return ResponseBuilder.Fail<string>("Habit Updating failed", "DeleteHabitAsync", 400);

            return ResponseBuilder.Success("Habit Updated ", "Existing Habit Updated Successfully", "HabitServices");


        }
        public async Task<ApiResponse<string>?> DeleteHabitAsync(int id)
        {
            var userId = _userFind.GetId();

            var exist = await _habitRepository.GetById(id) ?? throw new NotFoundException("Habit Id Not Found!");
            if (exist.UserId != userId) throw new ForbiddenException("No Access to this Id ! Id miss match");

            bool res = await _habitRepository.Delete(id);
            if(!res) return ResponseBuilder.Fail<string>("Habit Deleting failed", "HabitServices", 400);
            return ResponseBuilder.Success("Habit Removed", "Habit Removed Successfully","HabitServices");

        }
        public async Task<ApiResponse<Habit>?> GetHabitAsync(int id)
        {
            var userId = _userFind.GetId();
            var data = await _habitRepository.GetById(id);
            if (data?.UserId != userId) throw new ForbiddenException("No Access to this Id ! Id miss match");
            return ResponseBuilder.Success(data, "habit fetched", "HabitServices");
        }
        public async Task<ApiResponse<IEnumerable<Habit?>>> GetUserHabitsAsync()
        {
            Guid userId = _userFind.GetId();
            var data = await _habitRepository.GetByUserId(userId);
            return ResponseBuilder.Success(data, "GetHabit", "HabitServices");

        }
        public async Task<ApiResponse<string>?> RecordCompletionAsync(int habitId, CompletionRequest completion)
        {
            var userId = _userFind.GetId();

            var exist = await _habitRepository.GetById(habitId) ?? throw new NotFoundException("Habit Id Not Found!");
            if (exist.UserId != userId) throw new ForbiddenException("No Access to this Id ! Id miss match");
            await _completionRepository.RecordCompletion(habitId, completion.Count, completion.Date, completion.Notes);
            return ResponseBuilder.Success("Habit Completion Added", "Habit Completion Recorded", "HabitServices");

        }
        public async Task<ApiResponse<IEnumerable<HabitCompletion?>>> GetCompletionsAsync(int habitId, DateTime startDate, DateTime endDate)
        {
          var completions = await _completionRepository.GetCompletions(habitId, startDate, endDate);
            return ResponseBuilder.Success(completions, "Fetched all the completions", "HabitServices");

        }
        public async Task<ApiResponse<HabitStats>?> GetHabitStatsAsync(int habitId)
        {
            var userId = _userFind.GetId();

            var habit = await _habitRepository.GetById(habitId) ?? throw new NotFoundException("Habit not found");
            if (habit.UserId != userId) throw new ForbiddenException("No Access to this Id ! Id miss match");

            var completions = await _completionRepository.GetById(habitId) ?? throw new NotFoundException("No Habit completions Found");
            var stats = new HabitStats();
            stats.TotalCompletions = completions.Sum(x => x.Count);

            var currentStreak = 0;
            var longestStreak = 0;
            var tempStreak = 0;
            DateTime? lastDate = null;

            foreach (var date in completions)
            {
                if (lastDate == null || (date.CompletionDate - lastDate.Value).Days == 1)
                {
                    tempStreak++;
                } 
                else
                {
                    tempStreak = 1;
                }
                if (tempStreak > longestStreak)
                {
                    longestStreak = tempStreak;
                }

                lastDate = date.CompletionDate;

                if (lastDate.HasValue && date.CompletionDate.Date == DateTime.Now.Date)
                {
                    currentStreak = tempStreak;
                }
            }

                stats.CurrentStreak = currentStreak;
                stats.LongestStreak= longestStreak;
                
                var ActiveDays = (DateTime.Today - habit.StartDate.Date).Days+1;
                var TargetCompletion =ActiveDays * habit.TargetCount;
                stats.CompletionPercentage = TargetCompletion > 0 ?
                ((decimal)stats.TotalCompletions / TargetCompletion) * 100 : 100;

                return ResponseBuilder.Success(stats, "User Habit Status Analyzed", "HabitServices");
        }

    }
}
