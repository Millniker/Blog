using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class TagDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
