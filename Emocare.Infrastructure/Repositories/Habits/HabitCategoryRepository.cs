using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Repositories.Habits
{
    public class HabitCategoryRepository:Repository<HabitCategory>, IHabitCategoryRepository
    {
        public HabitCategoryRepository(AppDbContext dbContext) : base(dbContext) { }

    }
}
