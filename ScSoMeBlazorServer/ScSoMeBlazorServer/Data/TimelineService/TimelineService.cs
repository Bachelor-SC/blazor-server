using ScSoMeBlazorServer.Data.LogActivityService;
using ScSoMeBlazorServer.Models;
using ScSoMeBlazorServer.Network;
using System.Text.Json;

namespace ScSoMeBlazorServer.Data.TimelineService
{
    public class TimelineService : ITimelineService
    {
        private readonly IAPIClient client;
        private ILogActivityService activityService;

        public TimelineService(IAPIClient client, ILogActivityService logActivity)
        {
            activityService = logActivity;
            this.client = client;
        }

        public async Task LikePost(string username, bool isLiked, int postID)
        {
            string isLikedAndPostID = "" + isLiked +", " + postID;
            await client.postToAPI($"Timeline/LikePost", isLikedAndPostID);
            await logLikeActivity(username);
        }

        public async Task LikeComment(string username, bool isLiked, int commentID)
        {
            string isLikedAndCommentID = "" + isLiked + ", " + commentID;
            await client.postToAPI($"Timeline/LikeComment", isLikedAndCommentID);
            await logLikeActivity(username);
        }

        public async Task AddComment(string username, string comment, int postID)
        {
            Comment commentToAdd = new Comment
            {
                username = username,
                comment = comment,
                likes = 0,
                postID = postID,
                createdDate = DateTime.Now,
            };
            await client.postToAPI($"Timeline/AddComment", commentToAdd);
            await logCommentActivity(username);
        }

        public async Task<List<Comment>> GetAllComments()
        {
            string json = await client.getFromAPI($"Timeline/GetAllComments");
            return createCommentsList(json);

            static List<Comment> createCommentsList(string json)
            {
                try
                {
                    List<Comment>? list = JsonSerializer.Deserialize<List<Comment>>(json);
                    return list ?? new List<Comment> {new Comment{
                    username = "Empty",
                    comment = "List returned empty",
                    likes = 0,
                    postID = 0,
                    createdDate = DateTime.Now,
                }
                };
                }
                catch (Exception ex)
                {
                    List<Comment> list = new List<Comment>();
                    list.Add(new Comment
                    {
                    username = "ERROR",
                    comment = ex.Message,
                    likes = 0,
                    postID = 0,
                    createdDate= DateTime.Now,

                    });
                    return list;

                }
            }
        }

        public async Task AddPost(string username, string content)
        {
            Post post = new Post
            {
                username = username,
                content = content,
                likes = 0,
                createdDate = DateTime.Now,
            };
            await client.postToAPI($"Timeline/AddPost", post);
            await logPostActivity(username);
        }

        public async Task<List<Post>> GetAllPosts(string username)
        {
            string json = await client.getFromAPI($"Timeline/GetAllPosts?username={username}");
            return createPostsList(json);

            static List<Post> createPostsList(string json)
            {
                try
                {
                    List<Post>? list = JsonSerializer.Deserialize<List<Post>>(json);
                    return list ?? new List<Post> {new Post{
                    username = "Empty",
                    content = "List returned empty",
                    likes = 0,
                    createdDate = DateTime.Now,
                }
                };
                }
                catch (Exception ex)
                {
                    List<Post> list = new List<Post>();
                    list.Add(new Post
                    {
                        username = "ERROR",
                        content = ex.Message,
                        likes = 0,
                        createdDate = DateTime.Now,
                    });
                    return list;
                }
            }
        }

        private async Task logCommentActivity(string activityTargetusername)
        {
            await activityService.LogActivity(new Activity
            {
                type = "Comment",
                action = "add",
                date = DateTime.Now,
                additionalInfo = $"The user {activityTargetusername} added a comment",
                username = activityTargetusername

            }, activityTargetusername);
        }

        private async Task logPostActivity(string activityTargetusername)
        {
            await activityService.LogActivity(new Activity
            {
                type = "Post",
                action = "add",
                date = DateTime.Now,
                additionalInfo = $"The user {activityTargetusername} posted something",
                username = activityTargetusername

            }, activityTargetusername);
        }

        private async Task logLikeActivity(string activityTargetusername)
        {
            await activityService.LogActivity(new Activity
            {
                type = "Like",
                action = "update",
                date = DateTime.Now,
                additionalInfo = $"The user {activityTargetusername} liked something",
                username = activityTargetusername

            }, activityTargetusername);
        }
    }
}
