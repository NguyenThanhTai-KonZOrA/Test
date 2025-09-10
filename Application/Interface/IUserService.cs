using Application.Request;
using Application.Response;
using Infrastructure.Models;

namespace Application.Interface
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
