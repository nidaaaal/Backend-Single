using Emocare.Domain.Entities.Task;
using Emocare.Domain.Interfaces.Repositories;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Emocare.Infrastructure.Repositories
{
    public class WellnessTaskRepository:Repository<WellnessTask>,IWellnessTaskRepository
    {
        public WellnessTaskRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<WellnessTask?> GetById(int id)=> await _dbSet.FindAsync(id);

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
