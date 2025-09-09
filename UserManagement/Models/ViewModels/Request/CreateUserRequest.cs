using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.ViewModels.Request
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
