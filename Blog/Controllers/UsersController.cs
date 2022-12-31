using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UsersController : Controller
    {
        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody] UserRegisterModel userRegisterModel)
        {

        }
        [HttpPost]
        [Route("login")]
        public async Task Login([FromBody] LoginCredential loginCredential)
        {

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
