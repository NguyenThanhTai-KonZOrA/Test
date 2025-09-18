using Application.Interface;
using Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenService _authenService;

        public AuthController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            // add commnet


            var user = _authenService.GetUserByUsername(login.Username);
            if (user == null || !_authenService.VerifyPassword(login.Password, user.Password))
            {
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });
            }

            var token = _authenService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
