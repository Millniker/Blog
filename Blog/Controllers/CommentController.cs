using Blog.DTO;
using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Linq.Expressions;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IIsValidToken _isValidToken;
        public CommentController(ICommentService commentService, IIsValidToken isValidToken)
        {
            _commentService = commentService;
            _isValidToken = isValidToken;
        }
        [HttpGet]
        [Route("{id}/tree")]
        public ActionResult<List<CommentDto>> GetComments(Guid id)
        {
            return _commentService.GetComments(id);
        }
        [HttpPost]
        [Route("{id}/comment")]
        [Authorize]
        public IActionResult AddComment(Guid id,[FromBody] CreateCommentDto createCommentDto)
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                _commentService.AddComment(id, createCommentDto, User.Identity.Name);
            }
            catch(Unauthorized)
            {

            }
             
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
