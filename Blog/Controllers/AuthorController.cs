using Blog.DTO;
using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthorController : Controller    
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<List<AuthorDto>> GetList()
        {
            try {
                return _authorService.GetAuthorList();
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
