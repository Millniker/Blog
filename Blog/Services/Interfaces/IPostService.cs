using Blog.DTO;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;

namespace Blog.Services.Interfaces
{
    public interface IPostService
    {
        public PostPagedListDto GetPosts(Guid[] tags, string author, Int32? min, Int32? max, PostSorting? sorting, Int32 page, Int32 size, string userId);
        public PostFullDto GetConcertPost (Guid concertPostId, string userId);
        public void SetLike(Guid postId, string userId);
        public void DeleteLike(Guid postId, string userId);

    }
}
