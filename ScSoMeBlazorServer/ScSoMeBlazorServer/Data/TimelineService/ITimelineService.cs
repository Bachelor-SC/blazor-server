using ScSoMeBlazorServer.Models;

namespace ScSoMeBlazorServer.Data.TimelineService
{
    public interface ITimelineService
    {
        public Task<List<Post>> GetAllPosts();
        public Task AddPost(string username, string content);

        public Task<List<Comment>> GetAllCommentsForAPost(int postID);

        public Task AddComment(string username, string comment, int postID);
    }
}
