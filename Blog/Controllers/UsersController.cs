using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/account/")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;
        public UsersController(IAuthService authService, IProfileService profileService)
        {
            _authService = authService;
            _profileService = profileService;   
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
        [Authorize]
        public async Task<Response> Logout()
        {
            return await _authService.LogoutUser(HttpContext);
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<UserDto> Profile()
        {
            return await _profileService.GetUserProfile(User.Identity.Name);
        }

        [HttpPut]
        [Route("profile")]
        [Authorize]
        public async Task UpdateProfile(UserEditModel userEditModel)
        {
             await _profileService.UpdateProfile(userEditModel, User.Identity.Name);
        }

    }
}
