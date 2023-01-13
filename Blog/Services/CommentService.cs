using Blog.DTO;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blog.Services
{
    public class CommentService: ICommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<CommentDto> GetComments(Guid commentId)
        {
            IQueryable<CommentsEntity> commentsEntities =_context.Comments.Where(x => x.ParentId == commentId);
            var comment = _context.Comments.Where(x=> x.Id == commentId);
            
            if (comment == null)
            {
                //trow
            }

            var CommentList =
                (from com in commentsEntities
                 select new CommentDto
                 {
                     Id = com.Id,
                     modifiedDate = com.ModifiedDate,
                     deleteDate = com.DeleteDate,
                     author = com.Author,
                     authorId = com.AuthorId,
                     content = com.Content,
                     subComments = com.SubComments,
                     
                 }).ToList();
            return CommentList;
        }
        public void AddComment (Guid postId,CreateCommentDto commentDto, string userId)
        {
            CommentsEntity commentsEntity = new CommentsEntity();
            var postEntity = _context.Post.Where(x => x.Id == postId).FirstOrDefault();
            if(postEntity == null)
            {
                //trow
            }
            var userEntity = _context.UserEntity.Where(x => x.Id.ToString() == userId).FirstOrDefault();
            if (commentDto.ParentId != null)

            {
                var parentComment = _context.Comments.Where(c => c.Id == commentDto.ParentId).FirstOrDefault();
                if(parentComment == null)
                {
                    //trow
                }
                parentComment.SubComments += 1;
                _context.Comments.Entry(parentComment).State = EntityState.Modified;
            }
            CommentsEntity comment = new CommentsEntity()
            {
                Id = Guid.NewGuid(),
                Content = commentDto.Content,
                ModifiedDate = null,
                DeleteDate = null,
                AuthorId = userEntity.Id,
                Author = userEntity.FullName,
                SubComments = 0,
                ParentId = commentDto.ParentId,
                Post = postEntity
            };
            postEntity.Comments.Add(comment);
            postEntity.CommentCount += 1;
            _context.Comments.Add(comment);
            _context.SaveChangesAsync();
            _context.Post.Entry(postEntity).State = EntityState.Modified;
            _context.SaveChangesAsync();

        }
        public void EditComment(Guid commentId, string content )
        {
            var comment = _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
            if (comment == null)
            {

            }
            if (comment.DeleteDate != null)
            {

            }

            comment.Content = content;
            comment.ModifiedDate = DateTime.Now;
            _context.Comments.Entry(comment).State = EntityState.Modified;
            _context.SaveChangesAsync();

           
        }
        public void DeleteComment(Guid commentId)
        {
            var comment = _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
            if (comment == null)
            {

            }
            var subComments = _context.Comments.Where(s => s.ParentId == commentId);
            if (subComments != null)
            {
                comment.DeleteDate = DateTime.Now;
                comment.ModifiedDate = DateTime.Now;
                comment.Content = "";
                _context.Comments.Entry(comment).State = EntityState.Modified;
            }
            else
            {
                _context.Comments.Remove(comment);
            }
            _context.SaveChangesAsync();


            
        }

    }
}
