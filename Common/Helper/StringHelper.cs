using System.Security.Cryptography;
using System.Text;

namespace Common.Helper
{
    public static class StringHelper
    {
        public static string PasswordHash(string password)
        {
            using var sha256 = SHA256.Create();
            var hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hash;
        }
    }
}
