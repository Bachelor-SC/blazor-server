namespace ScSoMeBlazorServer.Models
{
    public class Post
    {
        public int postID { get; set; }
        public string username { get; set; }
        public string content { get; set; }
        public int likes { get; set; }
        public DateTime? createdDate { get; set; }

        public bool commentsVisible = false;

        public bool isLiked = false;

    }
}
