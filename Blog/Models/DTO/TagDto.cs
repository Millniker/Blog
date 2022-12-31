using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTO
{
    public class TagDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string name { get; set; }
    }
}
