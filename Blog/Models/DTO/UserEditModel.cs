﻿using Blog.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class UserEditModel
    {
        [Required]
        [MinLength(6)]
        [RegularExpression(@"[a-zA-Z]+\w*@[a-zA-Z]+\.[a-zA-Z]+")]
        public string email { get; set; }
        
        [Required]
        [MinLength(1)]
        public string fullName { get; set; }
        public DateTime birthDate { get; set; }
        [Required]
        public Gender gender { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        
    }
}