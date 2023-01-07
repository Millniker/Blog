using Blog.DTO;
using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/post/")]
    [ApiController]
    public class PostController : Controller
    {
        [HttpGet]
        
        public async Task<PostPagedListDto> GetPosts([FromQuery] Array tags, string author, Int32 min, Int32 max, string sorting, Int32 page, Int32 size)
        {
            return null;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<PostFullDto> GetConcretPost([FromBody]Guid id)
        {
            return null;
        }
        [HttpPost]
        [Route("{postId}/like")]
        public  Task AddLike([FromBody] Guid id)
        {
            return null;
        }
        [HttpDelete]
        [Route("{postId}/like")]
        public Task DelLike([FromBody] Guid id)
        {
            return null;
        }
    }
}
