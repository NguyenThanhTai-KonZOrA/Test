using UserManagement.Models.Entity;

namespace UserManagement.Services.Interface
{
    public interface IAuthenService
    {
        User GetUserByUsername(string username);
        bool VerifyPassword(string password, string passwordHash);
        string GenerateJwtToken(User user);
    }
}
