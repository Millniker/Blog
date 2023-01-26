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
        public DateTime CreatedDate { get; set; }
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        public string Description { get; set; }
        [Required]
        public Int32 ReadingTime { get; set; }
        public string? Image { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        [MinLength(1)]
        public string Author { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int32 Likes { get; set; }

        [Required]
        [DefaultValue (false)]
        public bool HasLike { get; set; }
        [Required, DefaultValue(0)]
        public Int32 CommentCount { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
