using Emocare.Domain.Entities.Task;

namespace Emocare.Domain.Interfaces.Repositories
{
    public interface IWellnessTaskRepository : IRepository<WellnessTask>
    {
        Task<WellnessTask?> GetById(int id);
        Task<bool> Delete(int id);

    }
}
