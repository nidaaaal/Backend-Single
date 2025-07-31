using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Infrastructure.Persistence;

namespace Emocare.Infrastructure.Repositories.User
{
    public class PsychologistRepository : Repository<PsychologistProfile>,IPsychologistRepository
    {
        public PsychologistRepository(AppDbContext appContext) :base(appContext) { }
    }
}
