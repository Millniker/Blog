using Blog.Exeption;
using Blog.Models;
using Blog.Services.Interfaces;
using System.Data;

namespace Blog.Services
{
    public class IsValidToken: IIsValidToken
    {
        private readonly ApplicationDbContext _context;
        public IsValidToken (ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public void CheckIsValidToken(string token)
        {
            var disActiveToken = _context.Tokens.Where(t => t.Token == token);
            if (disActiveToken.Any())
            {
                throw new AuthenticationUserException();
            }
        }
    }
}
