using Emocare.Application.DTOs.Habits;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.Habits
{
    public class HabitRepository:Repository<Habit>,IHabitRepository
    {
        public HabitRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<Habit?>> GetByUserId(Guid userId)=>await _dbSet.Where(x=>x.UserId==userId).ToListAsync();
        public async Task<Habit?> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

    }
}
