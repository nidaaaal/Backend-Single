using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Repositories.User
{
    public class PasswordHistoryRepo:IPasswordHistoryRepo
    {
        private readonly AppDbContext _appDbContext;
        public PasswordHistoryRepo(AppDbContext appDbContext) { _appDbContext = appDbContext; }

        public async Task<PasswordHistory?> PreviousPassword(Guid Id)
        {
           return await _appDbContext.PasswordHistory.OrderByDescending(x => x.Created).Skip(1).FirstOrDefaultAsync(x => x.UserId == Id);
        } 
        public async Task Add(PasswordHistory history)
        {
            _appDbContext.Add(history);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
