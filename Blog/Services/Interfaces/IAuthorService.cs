using Blog.DTO;

namespace Blog.Services.Interfaces
{
    public interface IAuthorService
    {
        public List<AuthorDto> GetAuthorList();

    }
}
