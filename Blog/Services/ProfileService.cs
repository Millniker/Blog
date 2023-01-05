using Blog.Models;
using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class ProfileService: IProfileService
    {
        private readonly ApplicationDbContext _context;
        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserDto> GetUserProfile(string id)
        {
            var userEntiry = await _context
                .UserEntity.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (userEntiry == null)
            {

            }
            var userDto = new UserDto
            {
                Id = userEntiry.Id,
                email = userEntiry.Email,
                fullName = userEntiry.fullName,
                birthDate = userEntiry.BirthDate,
                gender = userEntiry.Gender,
                phoneNumber = userEntiry.PhoneNumber
            };
            return userDto;
        }
        public async Task<UserEditModel>
    }
}
