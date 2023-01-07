using Blog.DTO;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class PostService: IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PostPagedListDto GetPosts(IList<TagEntity> tags, string author, Int32? min, Int32? max, PostSorting? sorting, Int32 page, Int32 size)
        {
            IQueryable<PostEntity> query = _context.Post;
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
            if (tags.Any())
            {
                query = query.
                    Where(post => tags.Contains(post.Tags));
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
                         select new PostDto {
                             Id = post.Id,
                             title = post.Title,
                             Description = post.Description,
                             readingTime = post.ReadingTime,
                             image = post.Image,
                             authotId = post.AuthorId,
                             authot = post.Author,
                             likes = post.Likes,
                             hasLike=post.HasLike,
                             commentCount = post.CommentCount,
                             Tags = new TagDto
                             {
                                 Id=post.Tags.Id,
                                 name = post.Tags.Name
                             },
                             Comments = new CommentDto
                             {
                                 Id = post.Comments.Id,
                                 content = post.Comments.content,
                                 modifiedDate = post.Comments.modifiedDate,
                                 deleteDate = post.Comments.deleteDate,
                                 author = post.Comments.author,
                                 authorId = post.Comments.authorId,
                                 subComments = post.Comments.subComments
                             }

                         }
                ).ToList(),
                Pagination = new PageInfoModel
                {
                    size = size,
                    count = pageCount,
                    current = page
                }
            };
            return PostList;
        }
        
    }
}