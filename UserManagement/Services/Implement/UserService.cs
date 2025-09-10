using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Data;
using UserManagement.Models.Entity;
using UserManagement.Models.ViewModels.Request;
using UserManagement.Models.ViewModels.Response;
using UserManagement.Services.Interface;

namespace UserManagement.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = await GetUserByUserNameAsync(createUserRequest.UserName);
            if (user != null)
            {
                throw new Exception("User have existed!");
            }
            var userCreated = new User()
            {
                UserName = createUserRequest.UserName,
                Address = createUserRequest.Address,
                Email = createUserRequest.Email,
                Name = createUserRequest.Name,
                Phone = createUserRequest.Phone,
                CreatedBy = "System",
                ModifiedBy = "System",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DistrictID = 0,
                GroupID = "System",
                Password = StringHashHelper.PasswordHash(createUserRequest.Password),
                ProvinceID = 0,
                Status = true,
                Id = Guid.NewGuid()
            };
            await _context.Users.AddAsync(userCreated);
            await _context.SaveChangesAsync();

            return new CreateUserResponse
            {
                Address = userCreated.Address,
                Email = userCreated.Email,
                Name = userCreated.Name,
                Phone = userCreated.Phone,
                UserName = userCreated.UserName
            };
        }

        public async Task<bool> DeleteUser(string userName)
        {
            var user = await GetUserByUserNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name.Equals(userName));
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest userRequest)
        {
            var user = await GetUserByEmailAsync(userRequest.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.UserName == userRequest.UserName)
            {
                throw new Exception("UserName have existed");
            }

            user.UserName = userRequest.UserName;
            user.Address = userRequest.Address;
            user.Name = userRequest.Name;
            user.Phone = userRequest.Phone;
            user.Password = StringHashHelper.PasswordHash(userRequest.Password);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new UpdateUserResponse()
            {
                Address = user.Address,
                Name = user.Name,
                Phone = user.Phone,
            };
        }

        public async Task<List<UsersFillterResponse>> GetAllUsersAsync()
        {
            var usersFillters = new List<UsersFillterResponse>();

            var users = await _context.Users.ToListAsync();
            foreach (var item in users)
            {
                usersFillters.Add(new UsersFillterResponse
                {
                    Address = item.Address,
                    Name = item.Name,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy,
                    Email = item.Email,
                    Id = item.Id,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedDate = item.ModifiedDate,
                    Phone = item.Phone,
                    UserName = item.UserName
                });
            }
            return usersFillters;
        }

        public async Task<List<UsersFillterResponse>> GettAllUsersFillterAsync(UsersFillterRequest usersFillterRequest)
        {
            List<UsersFillterResponse> usersFillters = new();
            if (usersFillterRequest == null)
            {
                return usersFillters;
            }

            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(usersFillterRequest.Address))
            {
                users = users.Where(x => x.Address == usersFillterRequest.Address);
            }

            if (!string.IsNullOrEmpty(usersFillterRequest.Name))
            {
                users = users.Where(x => x.Name == usersFillterRequest.Name);
            }

            if (usersFillterRequest.CreatedDateFrom.HasValue && usersFillterRequest.CreatedDateTo.HasValue)
            {
                users = users.Where(x => x.CreatedDate >= usersFillterRequest.CreatedDateFrom.GetValueOrDefault()
                && x.CreatedDate <= usersFillterRequest.CreatedDateTo.GetValueOrDefault());
            }

            //if (usersFillterRequest.CreatedDateTo.HasValue)
            //{
            //    users = users.Where(x => x.CreatedDate <= usersFillterRequest.CreatedDateTo.GetValueOrDefault());
            //}

            var usersAfterQueries = await users.ToListAsync();

            foreach (var item in usersAfterQueries)
            {
                usersFillters.Add(new UsersFillterResponse
                {
                    Address = item.Address,
                    Name = item.Name,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy,
                    Email = item.Email,
                    Id = item.Id,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedDate = item.ModifiedDate,
                    Phone = item.Phone,
                    UserName = item.UserName
                });
            }
            return usersFillters;
        }

        private bool IsExistedUserName(string userName)
        {
            return _context.Users.Any(x => x.UserName.Equals(userName));
        }


    }
}
