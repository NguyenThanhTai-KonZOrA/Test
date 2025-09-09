using UserManagement.Models.Entity;
using UserManagement.Models.ViewModels.Request;
using UserManagement.Models.ViewModels.Response;

namespace UserManagement.Services.Interface
{
    public interface IUserService
    {
        Task<User> GetUserByNameAsync(string userName);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string email);
        Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest userRequest);
        Task<bool> DeleteUser(string userName);

        Task<List<UsersFillterResponse>> GetAllUsersAsync();
        Task<List<UsersFillterResponse>> GettAllUsersFillterAsync(UsersFillterRequest usersFillterRequest);
    }
}
