﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Entities
{
    public class TokenEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
    }
}
