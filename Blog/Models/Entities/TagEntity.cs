using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Entities
{
    public class TagEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]

        public string Name { get; set; }
    }
}
