using Blog.Models.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public ActionResult<List<TagDto>> Gettag()
        {
            try
            {
                return _tagService.GetTegs();

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
