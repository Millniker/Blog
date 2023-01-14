using Blog.Models;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class IsValidToken: IIsValidToken
    {
        private readonly ApplicationDbContext _context;
        public IsValidToken (ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public bool CheckIsValidToken(string token)
        {
            var disActiveToken = _context.Tokens.Where(t => t.Token == token);
            if (disActiveToken.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
