using Blog.DTO;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            if (tags.Any() && tags != null)
            {
                foreach (var tag in tags)
                {
                    query = query.Where(p => p.Tags
                    .Select(k => k.Id).
                    Contains(tag));

                }

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

            if (page > pageCount)
            {
                //  throw
            }
            List<PostEntity> posts = query.Skip(size * (page - 1)).Take(size).ToList();
            var PostList = new PostPagedListDto
            {
                Posts = (from post in posts
                         select new PostDto
                         {
                             Id = post.Id,
                             title = post.Title,
                             Description = post.Description,
                             readingTime = post.ReadingTime,
                             image = post.Image,
                             authotId = post.AuthorId,
                             authot = post.Author,
                             likes = post.Likes,
                             hasLike = HasLike(userId, post.Id),

                             commentCount = post.CommentCount,
                             Tags = (from tag in post.Tags
                                     select new TagDto
                                     {
                                         Id =tag.Id,
                                         name = tag.Name

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
        public PostFullDto GetConcertPost (Guid id)
        {

            var post = _context.Post
                .Include(t => t.Tags)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (post == null)
            {

            }
            List<CommentDto> comments = GetCommentsList(id);
            return new PostFullDto
            {

                Id = post.Id,
                title = post.Title,
                Description = post.Description,
                readingTime = post.ReadingTime,
                image = post.Image,
                authorId = post.AuthorId,
                author = post.Author,
                likes = post.Likes,
                hasLike = post.HasLike,

                commentCount = post.CommentCount,
                tags = (from tag in post.Tags
                        select new TagDto
                        {
                            Id = tag.Id,
                            name = tag.Name

                        }).ToList(),
                comments = comments,
               
                
            };
        }
        public List<CommentDto> GetCommentsList (Guid postId)
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
       
        public Response SetLike(Guid postId, string userId)
        {
            
            var post = _context.Post.Where(p => p.Id == postId).FirstOrDefault();
            var author = _context.UserEntity.Where(a => a.Id == post.AuthorId).FirstOrDefault();
            author.Likes += 1;
            if (post == null)
            {

            }
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
                _context.UsersLikedPosts.AddAsync(usersLiked);
                _context.SaveChangesAsync();
               
            }
            else
            {
                //trow
            }
            Response result = new()
            {
                status = "200",
                message = "Success"

            };


            return result;
        }
        public Response DeleteLike(Guid postId, string userId)
        {
            var post = _context.Post.Where(p => p.Id == postId).FirstOrDefault();
            var author = _context.UserEntity.Where(a => a.Id == post.AuthorId).FirstOrDefault();
            author.Likes -= 1;
            if (post == null)
            {

            }
            var userLikedPost = HasLike(userId, postId);
            if (userLikedPost)
            {
                post.Likes -= 1;
                UsersLikedPost usersLiked = _context.UsersLikedPosts.Where(x => x.UserId.ToString() == userId && x.PostId == postId).FirstOrDefault();
                _context.Attach(post).State = EntityState.Modified;
                _context.UsersLikedPosts.Remove(usersLiked);
                _context.SaveChangesAsync();

            }
            else
            {
                //trow
            }
            Response result = new()
            {
                status = "200",
                message = "Success"

            };


            return result;
        }
    }
}