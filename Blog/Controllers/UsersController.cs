using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAuthService _authService;
        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<TokenResponse> Register([FromBody] UserRegisterModel userRegisterModel)
        {
           return await _authService.RegisterUser(userRegisterModel);
        }
        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> Login([FromBody] LoginCredential loginCredential)
        {
            return await _authService.LoginUser(loginCredential);
        }
        [HttpPost]
        [Route("logout")]
        public async Task Logout()
        {

        }

        [HttpGet]
        [Route("profile")]
        public async Task<UserDto> Profile()
        {
            return null;
        }

        [HttpPut]
        [Route("profile")]
        public async Task<UserEditModel> UpdateProfile()
        {
            return null;
        }

    }
}
