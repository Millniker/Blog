using Blog.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MinLength(6)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [DefaultValue(0)]
        public Int32 Likes { get; set; }
        public Int32 CreatedPosts { get; set; }
    }
}
