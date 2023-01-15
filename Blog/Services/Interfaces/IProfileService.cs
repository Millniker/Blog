using Blog.Models.DTO;

namespace Blog.Services.Interfaces
{
    public interface IProfileService
    {
        public UserDto GetUserProfile(string id);
        public void UpdateProfile(UserEditModel userEditModel, string id);
    }
}
