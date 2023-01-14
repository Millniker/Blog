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
        public IActionResult UpdateComment(Guid id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            _commentService.EditComment(id, updateCommentDto);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteComment(Guid id)
        {
            _commentService.DeleteComment(id);
            return Ok();
        }

    }
}
