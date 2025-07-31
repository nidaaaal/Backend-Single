using Emocare.Application.DTOs.Reflection;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/reflection")]
    [ApiController]
    public class ReflectionController : ControllerBase
    {
        private readonly IReflectionServices _reflectionServices;
        public ReflectionController(IReflectionServices reflectionServices) 
        {
            _reflectionServices = reflectionServices;
        }


        [HttpPost("Daily")]
        public async Task<IActionResult> GetReflection(DailyJournalDto dto)
            => Ok(await _reflectionServices.GetReflection(dto.prompt,dto.mood));


        [HttpGet("Daily")]
        public async Task<IActionResult> GetToday()
            => Ok(await _reflectionServices.DailyReflection());

        [HttpGet("Weekly")]
        public async Task<IActionResult> GetWeek()
            => Ok(await _reflectionServices.LastWeekDailyReflection());
    }
}
