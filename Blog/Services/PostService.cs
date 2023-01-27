using Blog.DTO;
using Blog.Exeption;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blog.Services
{

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;

        }

        public PostPagedListDto GetPosts(Guid[]? tags, string? author, Int32? min, Int32? max, PostSorting? sorting, Int32 page, Int32 size, string userId)
        {
            IQueryable<PostEntity> query = _context.Post.Include(one => one.Tags);

            foreach (var tag in tags)
            {

                query = query.Where(p => p.Tags
                    .Select(k => k.Id).
                    Contains(tag));

            }


            var tagsss = _context.Post.Include(p => p.Tags);
            if (min != null)
            {
                query = query.
                    Where(post => post.ReadingTime >= min);
            }
            if (max != null)
            {
                query = query.
                    Where(post => post.ReadingTime <= max);
            }

            if (sorting != null)
            {
                switch (sorting)
                {
                    case PostSorting.CreateAsc:
                        query = query.OrderBy(post => post.Created);
                        break;
                    case PostSorting.CreatDesc:
                        query = query.OrderByDescending(post => post.Created);
                        break;
                    case PostSorting.LikeAsc:
                        query = query.OrderBy(post => post.Likes);
                        break;
                    case PostSorting.LikeDesc:
                        query = query.OrderByDescending(post => post.Likes);
                        break;
                }
            }
            if (author != null)
            {
                query = query.
                    Where(a => a.Author.Contains(author));
            }
            int totalPostsCount = query.Count();
            int pageCount = (int)Math.Ceiling(totalPostsCount / (decimal)size);

            if (page > pageCount && pageCount != 0)
            {
                throw new PageNotFoundException();
            }
            List<PostEntity> posts = query.Skip(size * (page - 1)).Take(size).ToList();
            var PostList = new PostPagedListDto
            {
                Posts = (from post in posts
                         select new PostDto
                         {
                             Id = post.Id,
                             CreatedDate = post.Created,
                             Title = post.Title,
                             Description = post.Description,
                             ReadingTime = post.ReadingTime,
                             Image = post.Image,
                             AuthorId = post.AuthorId,
                             Author = post.Author,
                             Likes = post.Likes,
                             HasLike = HasLike(userId, post.Id),

                             CommentCount = post.CommentCount,
                             Tags = (from tag in post.Tags
                                     select new TagDto
                                     {
                                         Id = tag.Id,
                                         Name = tag.Name,
                                         CreatedDate = tag.CreatedDate

                                     }).ToList(),
                         }).ToList(),
                Pagination = new PageInfoModel
                {
                    size = size,
                    count = pageCount,
                    current = page
                }
            };
            return PostList;
        }
        public bool HasLike(string user, Guid post)
        {
            var usersLikedPost = _context.UsersLikedPosts.Where(x => x.UserId.ToString() == user && x.PostId == post).FirstOrDefault();
            if (usersLikedPost != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public PostFullDto GetConcertPost(Guid id, string userId)
        {

            var post = _context.Post
                .Include(t => t.Tags)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (post == null)
            {
                throw new PageNotFoundException();
            }
            List<CommentDto> comments = GetCommentsList(id);
            return new PostFullDto
            {

                Id = post.Id,
                CreatedDate = post.Created,
                title = post.Title,
                Description = post.Description,
                readingTime = post.ReadingTime,
                image = post.Image,
                authorId = post.AuthorId,
                author = post.Author,
                likes = post.Likes,
                hasLike = HasLike(userId, post.Id),

                commentCount = post.CommentCount,
                tags = (from tag in post.Tags
                        select new TagDto
                        {
                            Id = tag.Id,
                            CreatedDate = tag.CreatedDate,
                            Name = tag.Name


                        }).ToList(),
                comments = comments,


            };
        }
        public List<CommentDto> GetCommentsList(Guid postId)
        {
            List<CommentsEntity> comments = _context.Comments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedTime).ToList();
            List<CommentDto> commentDtos = new();
            foreach (var com in comments)
            {
                if (com.ParentId == null)
                {
                    commentDtos.Add(new CommentDto
                    {
                        Id = com.Id,
                        modifiedDate = com.ModifiedDate,
                        deleteDate = com.DeleteDate,
                        author = com.Author,
                        authorId = com.AuthorId,
                        content = com.Content,
                        subComments = com.SubComments
                    });

                }
            }
            return commentDtos;

        }

        public void SetLike(Guid postId, string userId)
        {

            var post = _context.Post.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
            {
                throw new PageNotFoundException();
            }
            var author = _context.UserEntity.Where(a => a.Id == post.AuthorId).FirstOrDefault();
            author.Likes += 1;

            var userLikedPost = HasLike(userId, postId);
            if (!userLikedPost)
            {
                post.Likes += 1;
                UsersLikedPost usersLiked = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = new Guid(userId),
                    PostId = postId,
                };
                _context.Post.Entry(post).State = EntityState.Modified;
                _context.UsersLikedPosts.Add(usersLiked);
                _context.SaveChanges();

            }
            else
            {
                throw new UserLikeExeption();
            }

        }
        public void DeleteLike(Guid postId, string userId)
        {
            var post = _context.Post.Where(p => p.Id == postId).FirstOrDefault();

            if (post == null)
            {
                throw new PageNotFoundException();

            }
            var author = _context.UserEntity.Where(a => a.Id == post.AuthorId).FirstOrDefault();
            author.Likes -= 1;
            var userLikedPost = HasLike(userId, postId);
            if (userLikedPost)
            {
                post.Likes -= 1;
                UsersLikedPost usersLiked = _context.UsersLikedPosts.Where(x => x.UserId.ToString() == userId && x.PostId == postId).FirstOrDefault();
                _context.Attach(post).State = EntityState.Modified;
                _context.UsersLikedPosts.Remove(usersLiked);
                _context.SaveChanges();

            }
            else
            {
                throw new UserLikeExeption();
            }

        }
    }
}