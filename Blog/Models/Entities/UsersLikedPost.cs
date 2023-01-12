namespace Blog.Models.Entities
{
    public class UsersLikedPost
    {
        public Guid Id { get; set; }    
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
