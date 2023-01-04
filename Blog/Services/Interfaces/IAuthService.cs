using Blog.Models.DTO;

namespace Blog.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<TokenResponse> LoginUser(LoginCredential loginCredentials);
        public Task<TokenResponse> RegisterUser(UserRegisterModel userRegisterModel);
    }
}
