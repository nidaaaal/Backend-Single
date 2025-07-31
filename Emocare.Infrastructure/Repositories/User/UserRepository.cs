using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Infrastructure.Repositories.User
{
    public class UserRepository: Repository<Users>,IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Users>> GetAllActive() => await _context.Users.Where(x => x.Role==UserRoles.User && x.Status==UserStatus.Active).ToListAsync();

        public async Task<Users?> GetByEmail(string email) => await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);

    }
}
