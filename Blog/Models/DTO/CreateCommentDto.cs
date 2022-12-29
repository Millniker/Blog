using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(1)]
        public string content { get; set; }
        public Guid parentId { get; set; }
    }
}
