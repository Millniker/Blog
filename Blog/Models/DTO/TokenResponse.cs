using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class TokenResponse
    {
        [Required]
        [MinLength(1)]
        public string token { get; set; }
    }
}
