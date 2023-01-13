using Blog.DTO;
using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        [Route("{id}/tree")]
        public ActionResult<List<CommentDto>> GetComments(Guid id)
        {
            return _commentService.GetComments(id);
        }
        [HttpPost]
        [Route("{id}/comment")]
        public IActionResult AddComment(Guid id,[FromBody] CreateCommentDto createCommentDto)
        {
             _commentService.AddComment(id, createCommentDto,User.Identity.Name);
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateComment(Guid id, [FromBody] UpdateCommentDto updateCommentDto)
        {

        }
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteComment(Guid id)
        {

        }

    }
}
