using Blog.DTO;
using Blog.Models;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        public AuthorService(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<AuthorDto> GetAuthorList()
        {
            var authors = _context.UserEntity.Where(a => a.CreatedPosts > 0);
            var authorList =
                (from author in authors
                 select new AuthorDto
                 {
                     FullName = author.FullName,
                     BirthDate = author.BirthDate,
                     Gender = author.Gender,
                     Posts = author.CreatedPosts,
                     Likes = author.Likes,
                     Created = author.Created
                 }).ToList();
            return authorList;

        }
    }
}
