using Common.Helper;
using Infrastructure.ApplicationDbContext;
using Infrastructure.Models;

namespace Infrastructure.Data_Initializer
{
    public static class DataInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Password = StringHelper.PasswordHash("123456"),
                    Address = "HCM",
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    Email = "aaa@gmail.com",
                    Status = true,
                    Phone = "090909",
                    Name = "admin",
                    GroupID = "Admin",
                    ModifiedBy = "System",
                    ModifiedDate = DateTime.Now,
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
