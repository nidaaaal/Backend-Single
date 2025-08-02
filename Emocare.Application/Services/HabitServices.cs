using AutoMapper;
using Emocare.Application.DTOs.Habits;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Enums.Habit;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Shared.Helpers.Api;
using System.Globalization;

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
        public async Task<ApiResponse<IEnumerable<HabitCompletion?>>> GetCompletionsAsync(int habitId)
        {
          var completions = await _completionRepository.GetCompletions(habitId);
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

            var completionDates = completions
            .Select(c => c.CompletionDate.Date)
            .Distinct()
            .OrderBy(d => d)
            .ToList();

            var currentStreak = 0;
            var longestStreak = 0;
            int activeDays = (DateTime.Today - habit.StartDate.Date).Days + 1;
            int targetCompletions = 0;

            if (habit.Frequency == Frequency.Daily)
            {
                int tempStreak = 0;
                DateTime? lastDate = null;



                foreach (var date in completionDates)
                {
                    if (lastDate == null || (date - lastDate.Value).Days == 1)
                        tempStreak++;

                    else if ((date - lastDate.Value).Days > 1)
                        tempStreak = 1;

                    if (tempStreak > longestStreak)
                        longestStreak = tempStreak;


                    if (lastDate.HasValue && date.Date == DateTime.Now.Date)
                        currentStreak = tempStreak;

                    lastDate = date;

                }

                targetCompletions = activeDays * habit.TargetCount;
            }
            else if (habit.Frequency == Frequency.Weekly)
            {
                int tempStreak = 0;
                int? lastWeek = null;
                int currentWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                                  DateTime.Today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);


                var groupedWeeks = completionDates
                                   .GroupBy(date => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                                   date,
                                   CalendarWeekRule.FirstFourDayWeek,
                                   DayOfWeek.Monday))
                                   .OrderBy(g => g.Key)
                                   .ToList();


                foreach (var weekGroup in groupedWeeks)
                {
                    int week = weekGroup.Key;

                    if (lastWeek == null || week == lastWeek + 1)
                        tempStreak++;

                    if (week > lastWeek)
                        tempStreak = 1;

                    if (tempStreak > longestStreak)
                        longestStreak = tempStreak;

                    if (week == currentWeek)
                        currentStreak = tempStreak;

                    lastWeek=week;
                }

                int activeWeeks = (int)Math.Ceiling(activeDays / 7.0);

                targetCompletions = activeWeeks * habit.TargetCount;
            }

                stats.CurrentStreak = currentStreak;
                stats.LongestStreak= longestStreak;
                
                stats.CompletionPercentage = targetCompletions > 0 ?
                ((decimal)stats.TotalCompletions / targetCompletions) * 100 : 100;

                return ResponseBuilder.Success(stats, "User Habit Status Analyzed", "HabitServices");
        }

    }
}
