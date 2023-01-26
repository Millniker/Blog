using Blog.Exeption;
using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Blog.Controllers
{
    [Route("api/account/")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;
        private readonly IIsValidToken _isValidToken;
        public UsersController(IAuthService authService, IProfileService profileService, IIsValidToken isValidToken)
        {
            _authService = authService;
            _profileService = profileService;
            _isValidToken = isValidToken;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<TokenResponse> Register([FromBody] UserRegisterModel userRegisterModel)
        {
            if((userRegisterModel.BirthDate - DateTime.Now).TotalMilliseconds > 0)
            {
                return BadRequest(new Response
                {   
                    status = "400",
                    message = "Birth date can't be later than today"
                });
            }
            try{
                return _authService.RegisterUser(userRegisterModel);
            }
            catch(DuplicateUserException)
            {
                return Conflict(new
                {

                    DuplicateUserName = new[]
                    {
                        $"Username '{userRegisterModel.Email}' is already taken"
                    }

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }

        }
        [HttpPost]
        [Route("login")]
        public ActionResult<TokenResponse> Login([FromBody] LoginCredential loginCredential)
        {
            try
            {

                return _authService.LoginUser(loginCredential);
            }
            catch (AuthenticationUserException)
            {
                return BadRequest (new Response
                {
                    status = null,
                    message = "Login faild"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }
        }
        [HttpPost]
        [Route("logout")]
        [Authorize]
        public  IActionResult Logout()
        {
            try
            {
                _authService.LogoutUser(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                return Ok();
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public ActionResult<UserDto> Profile()
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                return _profileService.GetUserProfile(User.Identity.Name);
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }
        }

        [HttpPut]
        [Route("profile")]
        [Authorize]
        public IActionResult UpdateProfile(UserEditModel userEditModel)
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                _profileService.UpdateProfile(userEditModel, User.Identity.Name);
                return Ok();
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (DuplicateUserEmail)
            {
                return Conflict(new
                {

                    DuplicateUserName = new[]
                    {
                        $"Username '{userEditModel.email}' is already taken"
                    }

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }

        }

    }
}
