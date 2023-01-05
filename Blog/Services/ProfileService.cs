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
        public async Task<UserDto> GetUserProfile(string id)
        {
            var userEntity = await _context
                .UserEntity.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (userEntity == null)
            {

            }
            var userDto = new UserDto
            {
                Id = userEntity.Id,
                email = userEntity.Email,
                fullName = userEntity.FullName,
                birthDate = userEntity.BirthDate,
                gender = userEntity.Gender,
                phoneNumber = userEntity.PhoneNumber
            };
            return userDto;
        }
        public async Task UpdateProfile(UserEditModel userEditModel, string id)
        {
            var user =  await _context
                .UserEntity
                .Where(x => x.Id.ToString() == id)
                .FirstOrDefaultAsync();

            /*if(user == null)
            {
                trow 
            }*/
            var userWhithSameEmail = await _context
                .UserEntity
                .Where(x => x.Email == userEditModel.email)
                .FirstOrDefaultAsync();

            
            if(userWhithSameEmail != null && userWhithSameEmail.FullName != user.FullName)
            {
                //trow
            }
            var userWhithSamefullName = await _context
               .UserEntity
               .Where(x => x.FullName == userEditModel.fullName)
               .FirstOrDefaultAsync();


            if (userWhithSamefullName != null && userWhithSamefullName.Email != user.Email)
            {
                //trow
            }
            var userWhithSamePhoneNumber = await _context
               .UserEntity
               .Where(x => x.PhoneNumber == userEditModel.PhoneNumber)
               .FirstOrDefaultAsync();


            if (userWhithSamefullName != null && userWhithSamefullName.PhoneNumber != user.PhoneNumber)
            {
                //trow
            }
            user.Email = userEditModel.email;
            user.FullName = userEditModel.fullName;
            user.BirthDate = userEditModel.birthDate;
            user.Gender = userEditModel.gender;
            user.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();


        }
    }
}
