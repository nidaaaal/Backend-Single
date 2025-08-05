using Emocare.Domain.Entities.Auth;


namespace Emocare.Domain.Interfaces.Helper.Auth
{
    public interface IJwtHelper
    {
        string GetJwtToken(Users user);
        string GenerateShortLivedToken(Guid userId);
    }
}
