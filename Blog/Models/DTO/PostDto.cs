using Blog.Models.DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class PostDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string title { get; set; }
        [Required]
        [MinLength(1)]
        public string Description { get; set; }
        [Required]
        public Int32 readingTime { get; set; }
        public string? image { get; set; }

        [Required]
        public Guid authotId { get; set; }
        [Required]
        [MinLength(1)]
        public string authot { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int32 likes { get; set; }

        [Required]
        [DefaultValue (false)]
        public bool hasLike { get; set; }
        [Required, DefaultValue(0)]
        public Int32 commentCount { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
