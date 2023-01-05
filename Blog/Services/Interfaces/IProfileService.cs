using Blog.Models.DTO;

namespace Blog.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<UserDto> GetUserProfile(string id);

    }
}
