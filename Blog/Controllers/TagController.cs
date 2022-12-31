using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TagController : Controller
    {
        [HttpGet]
        public async Task<TagDto> Gettag()
        {
            return null;
        }
    }
}
