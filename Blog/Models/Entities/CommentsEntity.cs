using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Entities
{
    public class CommentsEntity
    {
        public Guid id { get; set; }
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
    }
}
