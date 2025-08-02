using Emocare.Domain.Entities.Habits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories.Habits
{
    public interface IHabitHabitCompletionRepository:IRepository<HabitCompletion>
    {
        Task<IEnumerable<HabitCompletion?>> GetCompletions(int id);
        Task RecordCompletion(int id, int count, DateTime date, string notes);
        Task<IEnumerable<HabitCompletion?>> GetById(int id);
    }
}
