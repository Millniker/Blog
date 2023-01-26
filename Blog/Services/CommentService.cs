using Blog.DTO;
using Blog.Exeption;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blog.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<CommentDto> GetComments(Guid commentId)
        {
            IQueryable<CommentsEntity> commentsEntities = _context.Comments.Where(x => x.ParentId == commentId).OrderBy(c => c.CreatedTime);
            var comment = _context.Comments.Where(x => x.Id == commentId);

            if (comment == null)
            {
                throw new CommentsNotFoundException();
            }
            if (commentsEntities.Count() == 0)
            {
                throw new CommentWithoutChilds();
            }
            List<CommentsEntity> allComments = new List<CommentsEntity>();

            foreach (var comm in commentsEntities)
            {

                allComments.Add(comm);
                List<CommentsEntity> subcomments = new List<CommentsEntity>();
                subcomments = GetAllComments(comm.Id, subcomments);
                foreach (var comme in subcomments)
                {
                    if (comme != null)
                    {
                        allComments.Add(comme);
                    }
                }
            }
            var CommentList =
                (from com in allComments
                 select new CommentDto
                 {
                     Id = com.Id,
                     modifiedDate = com.ModifiedDate,
                     CreatedDate = com.CreatedTime,
                     deleteDate = com.DeleteDate,
                     author = com.Author,
                     authorId = com.AuthorId,
                     content = com.Content,
                     subComments = com.SubComments,

                 }).ToList();

            return CommentList;
        }
        public List<CommentsEntity> GetAllComments(Guid parentID, List<CommentsEntity> subcomments)
        {
            if (parentID == null)
            {
                return subcomments;
            }
            IQueryable<CommentsEntity> newComments = _context.Comments.Where(x => x.ParentId == parentID).OrderBy(c => c.CreatedTime);
            foreach (var com in newComments)
            {
                subcomments.Add(com);
                GetAllComments(com.Id, subcomments);
            }
            return subcomments;
        }
        public void AddComment(Guid postId, CreateCommentDto commentDto, string userId)
        {
            CommentsEntity commentsEntity = new CommentsEntity();
            var postEntity = _context.Post.Where(x => x.Id == postId).FirstOrDefault();
            if (postEntity == null)
            {
                throw new PostNotFoundExeption();
            }
            var userEntity = _context.UserEntity.Where(x => x.Id.ToString() == userId).FirstOrDefault();
            if (commentDto.ParentId != null)

            {
                var parentComment = _context.Comments.Where(c => c.Id == commentDto.ParentId).FirstOrDefault();
                if (parentComment == null || parentComment.DeleteDate != null)
                {
                    throw new CommentsNotFoundException();
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
                Post = postEntity,
                CreatedTime = DateTime.Now
            };
            postEntity.Comments.Add(comment);
            postEntity.CommentCount += 1;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            _context.Post.Entry(postEntity).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public void EditComment(Guid commentId, UpdateCommentDto updateCommentDto, string userId)
        {
            var comment = _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
            if (comment.AuthorId != new Guid(userId))
            {
                throw new ForbiddenException();
            }
            if (comment == null)
            {
                throw new CommentsNotFoundException();
            }
            if (comment.DeleteDate != null)
            {
                throw new CommentsNotFoundException();
            }

            comment.Content = updateCommentDto.content;
            comment.ModifiedDate = DateTime.Now;
            _context.SaveChanges();


        }
        public void DeleteComment(Guid commentId, string userId)
        {
            var comment = _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();

            if (comment.AuthorId != new Guid(userId))
            {
                throw new ForbiddenException();
            }
            if (comment == null)
            {
                throw new CommentsNotFoundException();
            }
            if (comment.DeleteDate != null)
            {
                throw new CommentsNotFoundException();
            }
            if (comment.ParentId != null)
            {
                var parentComment = _context.Comments.Where(s => s.Id == comment.ParentId).FirstOrDefault();
                parentComment.SubComments -= 1;
            }
            if (comment.SubComments != 0)
            {
                comment.DeleteDate = DateTime.Now;
                comment.ModifiedDate = DateTime.Now;
                comment.Content = "";
            }
            else
            {
                _context.Comments.Remove(comment);
            }
            _context.SaveChanges();



        }

    }
}
