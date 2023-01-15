using Blog.DTO;
using Blog.Models.DTO;

namespace Blog.Services.Interfaces
{
    public interface ICommentService
    {
        public List<CommentDto> GetComments(Guid commentId);
        public void AddComment(Guid postId, CreateCommentDto createComment, string userId);
        public void DeleteComment(Guid commentId,string userId);
        public void EditComment(Guid commentId, UpdateCommentDto updateCommentDto, string userId);

        

    }
}
