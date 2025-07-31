using Emocare.Domain.Entities.Auth;

namespace Emocare.Domain.Interfaces.Repositories.User
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<IEnumerable<Users>> GetAllActive();

        Task<Users?> GetByEmail(string email);


    }
}
