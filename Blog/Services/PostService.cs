using Blog.Models;

namespace Blog.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
