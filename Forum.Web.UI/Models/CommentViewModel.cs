namespace Forum.Web.UI.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; } // Optional for displaying the commenter's name
    }
}
