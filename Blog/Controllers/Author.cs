﻿using Blog.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Author : Controller    
    {
        [HttpGet]
        [Route("/list")]
        public async Task<AuthorDto> GetList()
        {
            return null;
        }
    }
}
