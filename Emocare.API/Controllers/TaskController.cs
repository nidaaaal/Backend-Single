using Emocare.Application.DTOs.Task;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Authorize(Policy="AdminOnly")]
    [Route("api/admin/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IWellnessTaskService _taskService;
        public TaskController(IWellnessTaskService wellnessTask)
        {
            _taskService = wellnessTask;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask()
            => Ok(await _taskService.GetTasksAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
            => Ok(await _taskService.GetTasksByIdAsync(id));


        [HttpPost]
        public async Task<IActionResult> AddTask(InsertTaskDto dto)
            => Ok(await _taskService.AddTaskAsync(dto));


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id,InsertTaskDto dto)
            => Ok(await _taskService.UpdateTaskAsync(id,dto));


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
            => Ok(await _taskService.DeleteTaskAsync(id));

    }
}
