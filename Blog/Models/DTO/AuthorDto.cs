using Blog.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class AuthorDto
    {
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public Int32 Posts { get; set; }
        public Int32 Likes { get; set; }
        public DateTime Created { get; set; }

    }
}
