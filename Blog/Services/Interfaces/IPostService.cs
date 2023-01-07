using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;

namespace Blog.Services.Interfaces
{
    public interface IPostService
    {
        public PostPagedListDto GetPosts(IList<TagEntity> tags, string author, Int32? min, Int32? max, PostSorting? sorting, Int32 page, Int32 size);
    }
}
