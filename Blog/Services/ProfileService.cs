using Blog.Exeption;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blog.Services
{
    public class ProfileService: IProfileService
    {
        private readonly ApplicationDbContext _context;
        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserDto GetUserProfile(string id)
        {
            var userEntity =  _context
                .UserEntity.FirstOrDefault(x => x.Id.ToString() == id);

            var userDto = new UserDto
            {
                Id = userEntity.Id,
                CreatedDate=userEntity.Created,
                email = userEntity.Email,
                fullName = userEntity.FullName,
                birthDate = userEntity.BirthDate,
                gender = userEntity.Gender,
                phoneNumber = userEntity.PhoneNumber
            };
            return userDto;
        }
        public void UpdateProfile(UserEditModel userEditModel, string id)
        {
            var user =   _context
                .UserEntity
                .Where(x => x.Id.ToString() == id)
                .FirstOrDefault();

            var userWhithSameEmail =  _context
                .UserEntity
                .Where(x => x.Email == userEditModel.email)
                .FirstOrDefault();



            if (userWhithSameEmail != null && userWhithSameEmail.Id != user.Id)
            {
                throw new DuplicateUserEmail();
            }
            
            user.Email = userEditModel.email;
            user.FullName = userEditModel.fullName;
            user.BirthDate = userEditModel.birthDate;
            user.Gender = userEditModel.gender;
            user.PhoneNumber = user.PhoneNumber;

             _context.SaveChanges();


        }
    }
}
