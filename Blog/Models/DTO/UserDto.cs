using Blog.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(6)]
        [EmailAddress]
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
