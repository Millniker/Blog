using Blog.Models.DTO;

namespace Blog.Services.Interfaces
{
    public interface IAuthService
    {
        public TokenResponse LoginUser(LoginCredential loginCredentials);
        public TokenResponse RegisterUser(UserRegisterModel userRegisterModel);
        public void LogoutUser(string token);
    }
}
