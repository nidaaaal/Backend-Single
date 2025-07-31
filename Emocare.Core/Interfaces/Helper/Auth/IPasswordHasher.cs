namespace Emocare.Domain.Interfaces.Helper.Auth
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedpassword);
    }
}
