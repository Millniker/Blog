using Blog.DTO;
using Blog.Exeption;
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
            try
            {
                return _commentService.GetComments(id);
            }
            catch (CommentsNotFoundException)
            {
                return NotFound(new Response
                {
                    status = "Error",
                    message = $"Comment with id={id} not found in database"
                });
            }
            catch (CommentWithoutChilds)
            {
                return BadRequest(new Response
                {
                    status = "Error",
                    message= $"Comment with id={id} is not parent element"
                }
                    
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }
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
                return Ok();
            }
            catch(AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (PostNotFoundExeption)
            {
                return NotFound(new Response {
                    status = "Error",
                    message = $"Post with id={id} not found in  database"
                });
            }
            catch (CommentsNotFoundException)
            {
                return NotFound(new Response
                {
                    status = "Error",
                    message = $"Comment with id={createCommentDto.ParentId} not found in  database"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public IActionResult UpdateComment(Guid id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                _commentService.EditComment(id, updateCommentDto, User.Identity.Name);
                return Ok();
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (CommentsNotFoundException)
            {
                return NotFound(new Response
                {
                    status = "Error",
                    message = $"Comment with id={id} not found in  database"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }

        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public IActionResult DeleteComment(Guid id)
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                _commentService.DeleteComment(id, User.Identity.Name);
                return Ok();
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (CommentsNotFoundException)
            {
                return NotFound(new Response
                {
                    status = "Error",
                    message = $"Comment with id={id} not found in  database"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {

                    status = "Error",
                    message = ex.Message,
                });
            }

        }

    }
}
