using AutoMapper;
using Emocare.Application.DTOs.Task;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Task;
using Emocare.Domain.Interfaces.Repositories;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Services
{
    public class WellnessTaskService:IWellnessTaskService
    {
        private readonly IWellnessTaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public WellnessTaskService(IWellnessTaskRepository taskRepository,IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;   
        }

        public async Task<ApiResponse<IEnumerable<WellnessTask>>> GetTasksAsync()
        {
            var task = await _taskRepository.GetAll();
            return ResponseBuilder.Success(task, "Task Fetched Successfully", "GetTasks");

        }
        public async Task<ApiResponse<WellnessTask>?> GetTasksByIdAsync(int id)
        {
            var task = await _taskRepository.GetById(id) ?? throw new NotFoundException("No Task Found On the Corresponding Id");
            return ResponseBuilder.Success(task, "Task Fetched Successfully", "GetTasks");
        }
        public async Task<ApiResponse<string>> AddTaskAsync(InsertTaskDto dto)
        {
            var task  = _mapper.Map<WellnessTask>(dto);
            await _taskRepository.Add(task);
            return ResponseBuilder.Success("Task Added", "New Task Added to WellnessTask", "AddTaskAsync");

        }
        public async Task<ApiResponse<string>?> UpdateTaskAsync(int id, InsertTaskDto dto)
        {
            var task = await _taskRepository.GetById(id) ?? throw new NotFoundException("No Task Found On the Corresponding Id");
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Category = dto.Category;   
            task.MoodTags = dto.MoodTags;   
           bool response = await _taskRepository.Update(task);
            if(!response) return ResponseBuilder.Fail<string>("Task Adding Failed", "AddTaskAsync", 500);

            return ResponseBuilder.Success("Task Updated", "Updated Task  and Added to WellnessTask", "UpdateTaskAsync");

        }
        public async Task<ApiResponse<string>?> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetById(id) ?? throw new NotFoundException("No Task Found On the Corresponding Id");
           bool response = await _taskRepository.Delete(id);
           if (!response) return ResponseBuilder.Fail<string>("Task Deleting Failed", "DeleteTaskAsync", 500);

            return ResponseBuilder.Success("Task Deleted Successfully", "Deleted Task and Added to WellnessTask", "DeleteTaskAsync");

        }
    }
}
