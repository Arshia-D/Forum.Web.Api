namespace Forum.Web.UI.Models
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; } 
    }
}
