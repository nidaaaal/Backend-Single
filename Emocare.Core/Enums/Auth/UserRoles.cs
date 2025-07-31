
namespace Emocare.Domain.Enums.Auth
{
    public enum UserRoles
    {
        User=1,
        Admin,
        Psychologist,
        Ai
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 2,
        Locked = 3,
        Banned = 4
    }

}
