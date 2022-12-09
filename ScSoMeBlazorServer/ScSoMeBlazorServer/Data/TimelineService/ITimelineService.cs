using ScSoMeBlazorServer.Models;

namespace ScSoMeBlazorServer.Data.TimelineService
{
    public interface ITimelineService
    {
        public Task<List<Post>> GetAllPosts();
        public Task AddPost(string username, string content);

        public Task<List<Comment>> GetAllComments();

        public Task AddComment(string username, string comment, int postID);

        public Task LikePost(string username, bool isLiked, int postID);

        public Task LikeComment(string username, bool isLiked, int commentID);
    }
}
