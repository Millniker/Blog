﻿using Blog.DTO;

namespace Blog.Models.DTO
{
    public class PostPagedListDto
    {
        public List<PostDto> Posts { get; set; }
        public PageInfoModel Pagination { get; set; }


    }
}
