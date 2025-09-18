using Application.Interface;
using Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUserByName")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("gettAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {


            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("gettAllUsersFillter")]
        public async Task<IActionResult> GettAllUsersFillter([FromBody] UsersFillterRequest userFillterRequest)
        {


            var users = await _userService.GettAllUsersFillterAsync(userFillterRequest);
            return Ok(users);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            var createUser = await _userService.CreateUser(createUserRequest);
            return Ok(createUser);
        }

        [HttpPatch("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            var updateUser = await _userService.UpdateUser(updateUserRequest);
            return Ok(updateUser);
        }
    }
}
