using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Services.Interfaces;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                fullName = userRegisterModel.fullName,
                Password = userRegisterModel.password,
                Email = userRegisterModel.email,
                BirthDate = userRegisterModel.birthDate,
                PhoneNumber = userRegisterModel.phoneNumber,
                Gender = userRegisterModel.gender,
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
        public async Task<Response> LogoutUser(HttpContext httpContext)
        {
            var token = GetToken(httpContext.Request.Headers);

            var handler = new JwtSecurityTokenHandler();
            var expiredDate = handler.ReadJwtToken(token).ValidTo;

            var tokenEntity = new TokenEntity
            {
                Id = Guid.NewGuid(),
                Token = token,
                ExpiredDate = expiredDate
            };

            await _context.Tokens.AddAsync(tokenEntity);
            await _context.SaveChangesAsync();


            var result = new Response()
            {
                status = "Logged out",
                message = token
            };
            return result;
        }
        private static string GetToken(IHeaderDictionary headerDictionary)
        {
            var requestHeaders = new Dictionary<string, string>();

            foreach (var header in headerDictionary)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            var authorizationString = requestHeaders["Authorization"];


            const string pattern = @"\S+\.\S+\.\S+";
            var regex = new Regex(pattern);
            var matches = regex.Matches(authorizationString);
            /*
                        if (matches.Count <= 0)
                        {
            throw new CanNotGetTokenException("Can not get the token from headers");
                        }*/

            return matches[0].Value;
        }

    }
}
