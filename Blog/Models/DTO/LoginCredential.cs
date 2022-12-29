using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class LoginCredential
    {
        [Required]
        [MinLength(1)]

        public string email { get; set; }
        [Required]
        [MinLength(1)]
        public string password { get; set; }
    }
}
