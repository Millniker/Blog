using Blog.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog.DTO
{
    public class PostFullDto
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [MinLength(1)]
        public string title { get; set; }
        [Required]
        [MinLength(1)]
        public string Description { get; set; }
        [Required]
        public Int32 readingTime { get; set; }
        public string image { get; set; }

        [Required]
        public Guid authorId { get; set; }
        [Required]
        [MinLength(1)]
        public string author{ get; set; }

        [Required]
        [DefaultValue(0)]
        public Int32 likes { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool hasLike { get; set; }
        [Required, DefaultValue(0)]
        public Int32 commentCount { get; set; }
        public List<TagDto> tags { get; set; }
        public List<CommentDto> comments { get; set; }

    }
}
