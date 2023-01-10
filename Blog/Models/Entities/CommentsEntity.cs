using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities
{
    public class CommentsEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string content { get; set; }

        public DateTime modifiedDate { get; set; }
        public DateTime deleteDate { get; set; }
        [Required]
        public Guid authorId { get; set; }
        [Required]
        [MinLength(1)]
        public string author { get; set; }
        [Required]
        public Int32 subComments { get; set; }
        public List<SubCommentsEntity> subCommentsEntities { get; set; }
        [ForeignKey("Post")]
        public Guid PostId { get; set; }

        public PostEntity Post { get; set; }
    }
}
