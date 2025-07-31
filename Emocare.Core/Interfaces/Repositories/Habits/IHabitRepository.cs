using Emocare.Domain.Entities.Habits;

namespace Emocare.Domain.Interfaces.Repositories.Habits
{
    public interface IHabitRepository:IRepository<Habit>
    {
        Task<Habit?> GetById(int id);   
        Task<IEnumerable<Habit?>> GetByUserId(Guid userId);
        Task<bool> Delete(int id);
    }
}
