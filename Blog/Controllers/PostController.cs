
using Blog.DTO;
using Blog.Exeption;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Models.Enums;
using Blog.Services;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Controllers
{
    [Route("api/post/")]
    [ApiController]
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        private readonly ApplicationDbContext _context;
        private readonly IIsValidToken _isValidToken;

        public PostController(IPostService postService, ApplicationDbContext context, IIsValidToken isValidToken)
        {
            _postService = postService;
            _context = context;
            _isValidToken = isValidToken;
        }
        [HttpGet]
        [ProducesResponseType(typeof(PostPagedListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<PostPagedListDto> GetPosts([FromQuery] Guid[]? tags, string? author = null, Int32? min = null, Int32? max = null, PostSorting? sorting = null, Int32 page = 1, Int32 size = 5)
        {
            foreach (var tag in tags)
            {
                var isValidTag = _context.Tags.Where(t => t.Id == tag);
                if (!isValidTag.Any())
                {
                    return NotFound(new Response
                    {
                        status = "Error",
                        message = $"Tag with id={tag} don't in database"
                    });
                }
            }

                try {
                return _postService.GetPosts(tags, author, min, max, sorting, page, size, User.Identity.Name);

            }
            catch (PageNotFoundException)
            {
                return BadRequest( new Response
                {
                    status ="400",
                    message = "Invalid value for attribute page"
                });
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<PostFullDto> GetConcretPost(Guid id)
        {
            try
            {
                return _postService.GetConcertPost(id);
            }
            catch(PageNotFoundException)
            {
                return NotFound(new Response
                {
                    status="Error",
                    message = $"Post with id={id} not found in database"
                });
            }
          
        }
        [HttpPost]
        [Route("{postId:guid}/like")]
        [Authorize]
        public IActionResult AddLike( Guid postId)
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                _postService.SetLike(postId, User.Identity.Name);
                return Ok();
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (UserLikeExeption)
            {
                return BadRequest(new Response{
                    status= "Error",
                    message= "Like on this post already set by user"
                });
            }
        }

        [HttpDelete]
        [Route("{postId:guid}/like")]
        [Authorize]
        public IActionResult DelLike(Guid postId)
        {
            try
            {
                _isValidToken.CheckIsValidToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
                _postService.DeleteLike(postId, User.Identity.Name);
                return Ok();
            }
            catch (AuthenticationUserException)
            {
                return Unauthorized();
            }
            catch (UserLikeExeption)
            {
                return BadRequest(new Response
                {
                    status = "Error",
                    message = "There are no like from user by this post"
                });
            }
           
        }
    }
}
