using Blog.DTO;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Controllers
{
    [Route("api/post/")]
    [ApiController]
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<PostPagedListDto> GetPosts([FromQuery] Guid[]? tags, string? author=null, Int32? min=null, Int32? max=null, PostSorting? sorting = null, Int32 page=1, Int32 size=5)
        {
            return _postService.GetPosts(tags, author, min, max, sorting, page, size,User.Identity.Name);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<PostFullDto> GetConcretPost( Guid id)
        {
            return _postService.GetConcertPost(id);
        }
        [HttpPost]
        [Route("{postId}/like")]
        [Authorize]
        public Response AddLike([FromBody] Guid id)
        {
            return _postService.SetLike(id,User.Identity.Name);
        }
        [HttpDelete]
        [Route("{postId}/like")]
        [Authorize]
        public Response DelLike([FromBody] Guid id)
        {
            return _postService.DeleteLike(id,User.Identity.Name);
        }
    }
}
