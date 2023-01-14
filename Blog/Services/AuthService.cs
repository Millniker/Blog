using Azure.Core;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Services.Interfaces;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
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
        public async Task<TokenResponse> RegisterUser(UserRegisterModel userRegisterModel)
        {
            //TODO добавить валидацию входных данных
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

            await _context.UserEntity.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            var loginCredentials = new LoginCredential
            {
                password = userEntity.Password,
                email = userEntity.Email
            };

            return await LoginUser(loginCredentials);
        }

        public async Task<TokenResponse> LoginUser(LoginCredential loginCredentials)
        {

            var identity = await GetIdentity(loginCredentials.email, loginCredentials.password);

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

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var userEntity = await _context
                .UserEntity
                .Where(x => x.Email == email && x.Password == password)
                .FirstOrDefaultAsync();
            /*
                        if (userEntity == null)
                        {

                        }*/

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

             _context.Tokens.AddAsync(tokenEntity);
             _context.SaveChangesAsync();
        }

    }
}
