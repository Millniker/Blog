﻿using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PostController : Controller
    {
        [HttpGet]
        public async Task<PostPagedListDto> GetPosts([FromQuery] Array tags, string author, Int32 min, Int32 max, string sorting, Int32 page, Int32 size)
        {
            return null;
        }
       
    }
}
