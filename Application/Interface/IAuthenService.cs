using Infrastructure.Models;

namespace Application.Interface
{
    public interface IAuthenService
    {
        User GetUserByUsername(string username);
        bool VerifyPassword(string password, string passwordHash);
        string GenerateJwtToken(User user);
    }
}
