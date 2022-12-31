using Blog.DTO;
using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Comment : Controller
    {
        [HttpGet]
        [Route("/{id}/tree")]
        public async Task<CommentDto> GetComments(Guid id)
        {
            return null;

        }
        [HttpPost]
        [Route("/{id}/comment")]
        public async Task AddComment(Guid id,[FromBody] CreateCommentDto createCommentDto)
        {
            
        }
        [HttpPut]
        [Route("/{id}")]
        public async Task UpdateComment(Guid id, [FromBody] UpdateCommentDto updateCommentDto)
        {

        }
        [HttpDelete]
        [Route("/{id}")]
        public async Task DeleteComment(Guid id)
        {

        }

    }
}
