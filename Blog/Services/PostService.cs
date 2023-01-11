using Blog.DTO;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Services
{

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;

        }

        public PostPagedListDto GetPosts(Guid[]? tags, string? author, Int32? min, Int32? max, PostSorting? sorting, Int32 page, Int32 size)
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
                    Where(post => post.Author.Contains(author));
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
                             hasLike = post.HasLike,

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
        public PostFullDto GetConcertPost (Guid id)
        {
            PostEntity post = _context.Post
                .Include(c => c.Comments)
                .Include(t => t.Tags)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (post == null)
            {

            }
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
                comments=(from com in post.Comments
                          select new CommentDto
                          {Id = com.Id,
                          content = com.content,
                          modifiedDate =com.modifiedDate,
                          deleteDate =com.deleteDate,
                          author = com.author,
                          authorId = com.authorId,
                          subComments = com.subComments,
                          }).ToList(),
                
            };
        }
    }
}