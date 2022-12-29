using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class CommentDto
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
        [MinLength (1)]
        public string author { get; set; }

        public Int32 subComments { get; set; }
    }
}
