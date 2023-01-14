using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities
{
    public class CommentsEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        [MinLength(1)]
        public string Author { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        public Int32 SubComments { get; set; }
        public Guid? ParentId {get; set; }
        [ForeignKey("Post")]
        public Guid PostId { get; set; }

        public PostEntity Post { get; set; }
    }
}
