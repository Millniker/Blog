using Blog.DTO;
using Blog.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog.Models.Entities
{
    public class PostEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        public string Description { get; set; }
        [Required]
        public Int32 ReadingTime { get; set; }
        public string Image { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        [MinLength(1)]
        public string Author { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int32 Likes { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool HasLike { get; set; }
        public DateTime Created { get; set; }
        [Required, DefaultValue(0)]
        public Int32 CommentCount { get; set; }
        public TagEntity Tags { get; set; }
        public CommentsEntity Comments { get; set; } 
    }
}
