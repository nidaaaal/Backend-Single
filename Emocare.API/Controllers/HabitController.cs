using Emocare.Application.DTOs.Habits;
using Emocare.Application.Interfaces;
using Emocare.Infrastructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/habit")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly IHabitServices _habitServices;

        public HabitController(IHabitServices habitServices)
        {
            _habitServices = habitServices;
        }

        [HttpGet]
        public async Task<IActionResult> UserHabit() => Ok(await _habitServices.GetUserHabitsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ById(int id) => Ok(await _habitServices.GetHabitAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(AddHabit habit) => Ok(await _habitServices.CreateHabitAsync(habit));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,AddHabit habit) => Ok(await _habitServices.UpdateHabitAsync(id,habit));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _habitServices.DeleteHabitAsync(id));

        [HttpGet("{id}/completion")]
        public async Task<IActionResult> Completion(int id,DateTime startDate,DateTime endDate) => Ok(await _habitServices.GetCompletionsAsync(id,startDate,endDate));

        [HttpPost("{id}/completion")]
        public async Task<IActionResult> Record(int id,CompletionRequest completion) => Ok(await _habitServices.RecordCompletionAsync(id,completion));

        [HttpGet("{id}/stats")]
        public async Task<IActionResult> Stats(int id) => Ok(await _habitServices.GetHabitStatsAsync(id));
    } 
}
