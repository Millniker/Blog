using Blog.DTO;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class CommentService: ICommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<CommentDto> GetComments(Guid postid)
        {
            var postEntity =_context.Post.Where(x => x.Id == postid).FirstOrDefault();
            if (postEntity == null)
            {

            }
            List<CommentsEntity> comments = _context.Comments.Where(x => x.PostId == postid).ToList();
            List<CommentDto> commentDtos = new List<CommentDto>();
            foreach (var comment in comments)
            {

            }


        }

    }
}
