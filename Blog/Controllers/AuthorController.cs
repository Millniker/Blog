using Blog.DTO;
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
            return _authorService.GetAuthorList();
        }
    }
}
