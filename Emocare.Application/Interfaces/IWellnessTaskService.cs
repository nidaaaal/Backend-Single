
using Emocare.Application.DTOs.Task;
using Emocare.Domain.Entities.Task;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IWellnessTaskService
    {
        Task<ApiResponse<IEnumerable<WellnessTask>>> GetTasksAsync();
        Task<ApiResponse<WellnessTask>?> GetTasksByIdAsync(int id);
        Task<ApiResponse<string>> AddTaskAsync(InsertTaskDto dto);
        Task<ApiResponse<string>?> UpdateTaskAsync(int id,InsertTaskDto dto);
        Task<ApiResponse<string>?> DeleteTaskAsync(int id);




    }
}
