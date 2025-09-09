using Common.Helper;
using UserManagement.Models.Entity;

namespace UserManagement.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Password = HashStringHelper.HashPassword("123456"),
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
