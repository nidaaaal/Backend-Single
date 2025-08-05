using Emocare.Domain.Entities.Auth;


namespace Emocare.Domain.Interfaces.Repositories.User
{
    public interface IPsychologistRepository : IRepository<PsychologistProfile>
    {
       Task<IEnumerable<Users>> GetAll();
    }
}
