using Blog.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class AuthorDto
    {
        [Required]
        [MinLength(1)]
        public string fullName { get; set; }

        public DateTime birthDate { get; set; }

        [Required]
        public Gender gender { get; set; }

        public Int32 posts { get; set; }
        public Int32 likes { get; set; }
        public DateTime created { get; set; }

    }
}
