using Emocare.Domain.Entities.Auth;


namespace Emocare.Domain.Interfaces.Repositories.User
{
    public interface IPasswordHistoryRepo
    {
        Task<PasswordHistory?> PreviousPassword(Guid Id);
        Task Add(PasswordHistory history);
    }
}
