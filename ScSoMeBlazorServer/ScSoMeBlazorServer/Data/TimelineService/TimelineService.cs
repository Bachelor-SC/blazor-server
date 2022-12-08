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

        public async Task AddComment(string username, string comment, int postID)
        {
            Comment commentToAdd = new Comment
            {
                username = username,
                comment = comment,
                likes = 0,
                postID = postID,
            };
            await client.postToAPI($"Timeline/AddComment", commentToAdd);
            await logActivity(username);
        }

        public async Task<List<Comment>> GetAllCommentsForAPost(int postID)
        {
            string json = await client.getFromAPI($"Timeline/CommentsForAPost?postID={postID}");
            return createCommentsList(json, postID);

            static List<Comment> createCommentsList(string json, int postID)
            {
                try
                {
                    List<Comment>? list = JsonSerializer.Deserialize<List<Comment>>(json);
                    return list ?? new List<Comment> {new Comment{
                    username = "Empty",
                    comment = "List returned empty",
                    likes = 0,
                    postID = postID,
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
                    postID = postID,

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
            };
            await client.postToAPI($"Timeline/AddPost", post);
            await logActivity(username);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            string json = await client.getFromAPI($"Timeline/GetAllPosts");
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
                    });
                    return list;
                }
            }
        }

        private async Task logActivity(string activityTargetusername)
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
    }
}
