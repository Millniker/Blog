using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(1)]
        public string contnet { get; set; }
    }
}
