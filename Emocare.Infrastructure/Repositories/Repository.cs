using Emocare.Domain.Interfaces.Repositories;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Emocare.Infrastructure.Repositories
{
  
        public class Repository<T> : IRepository<T> where T : class
        {
            protected readonly AppDbContext _context;
            protected readonly DbSet<T> _dbSet;

            public Repository(AppDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

            public async Task<T?> GetById(Guid id) => await _dbSet.FindAsync(id);

            public async Task<bool> Add(T entity)
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> Update(T entity)
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> Delete(Guid id)
            {
                var entity = await GetById(id);
                if (entity == null) return false;
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }


