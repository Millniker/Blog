using Azure.Core;
using Blog.Exeption;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Services.Interfaces;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Blog.Services
{
    public class AuthService: IAuthService
    {
        private readonly ApplicationDbContext _context;
        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }
        public TokenResponse RegisterUser(UserRegisterModel userRegisterModel)
        {
            if (_context.UserEntity.Any(user => user.Email == userRegisterModel.Email))
            {
                throw new DuplicateUserException();
            }
            var userEntity = new UserEntity
            {
                Id = Guid.NewGuid(),
                FullName = userRegisterModel.FullName,
                Password = userRegisterModel.Password,
                Email = userRegisterModel.Email,
                BirthDate = userRegisterModel.BirthDate,
                PhoneNumber = userRegisterModel.PhoneNumber,
                Gender = userRegisterModel.Gender,
                Created= DateTime.UtcNow

        };

             _context.UserEntity.Add(userEntity);
             _context.SaveChanges();

            var loginCredentials = new LoginCredential
            {
                password = userEntity.Password,
                email = userEntity.Email
            };

            return  LoginUser(loginCredentials);
        }

        public  TokenResponse LoginUser(LoginCredential loginCredentials)
        {

            var identity =  GetIdentity(loginCredentials.email, loginCredentials.password);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: JwtConfigurations.Issuer,
                audience: JwtConfigurations.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMinutes(JwtConfigurations.Lifetime),
                signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));


            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


            var result = new TokenResponse()
            {
                token = encodeJwt
            };

            return result;
        }

        private  ClaimsIdentity GetIdentity(string email, string password)
        {
            var userEntity =  _context
                .UserEntity
                .Where(x => x.Email == email && x.Password == password)
                .FirstOrDefault();

            if (userEntity == null)
            {
                throw new AuthenticationUserException();
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userEntity.Id.ToString()),
        };

            var claimsIdentity = new ClaimsIdentity
            (
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
        public void  LogoutUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var expiredDate = handler.ReadJwtToken(token).ValidTo;

            var tokenEntity = new TokenEntity
            {
                Id = Guid.NewGuid(),
                Token = token,
                ExpiredDate = expiredDate
            };

             _context.Tokens.Add(tokenEntity);
             _context.SaveChanges();
        }

    }
}
