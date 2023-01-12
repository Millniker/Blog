using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
    }
}
