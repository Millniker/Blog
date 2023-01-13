using Blog.DTO;

namespace Blog.Services.Interfaces
{
    public interface ICommentService
    {
        public List<CommentDto> GetComments(Guid commentId);
        public void AddComment(Guid postId, CreateCommentDto createComment, string userId);

    }
}
