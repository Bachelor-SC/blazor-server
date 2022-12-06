using ScSoMeBlazorServer.Models;
using ScSoMeBlazorServer.Network;
using System.Text.Json;

namespace ScSoMeBlazorServer.Data.LogActivityService
{
    public class LogActivityService : ILogActivityService
    {
        private readonly IAPIClient client;

        public LogActivityService(IAPIClient client)
        {
            this.client = client;
        }
        public async Task<List<Activity>> GetActivities(string user)
        {
            string json = await client.getFromAPI($"Activity/AllActivities?user={user}");

            return createList(json, user);

            static List<Activity> createList(string json, string user)
            {
                try
                {
                    List<Activity>? list = JsonSerializer.Deserialize<List<Activity>>(json);
                    return list ?? new List<Activity> {new Activity{
                    type = "Empty",
                    action = "",
                    date = DateTime.Now,
                    additionalInfo = "List returned empty",
                    username= user
                    
                }
                };
                }
                catch (Exception ex)
                {
                    List<Activity> list = new List<Activity>();
                    list.Add(new Activity
                    {
                        type = "Error",
                        action = "",
                        date = DateTime.Now,
                        additionalInfo = ex.Message,
                        username= user
                    });
                    return list;

                }
            }
        }

        public async  Task<Activity> GetActivity(string user)
        {
            string json = await client.getFromAPI($"Activity/LatestActivity?user={user}");

            return loadActivity(json, user);

            static Activity loadActivity(string json, string user)
            {
                try
                {
                    Activity? activity = JsonSerializer.Deserialize<Activity>(json);
                    return activity ?? new Activity
                    {
                    type = "Empty",
                    action = "",
                        date = DateTime.Now,
                    additionalInfo = "Activity returned empty",
                    username= user
                    };
                }
                catch (Exception ex)
                {
                    Activity activity = new Activity
                    {
                        type = "Error",
                        action = "",
                        date = DateTime.Now,
                        additionalInfo = ex.Message,
                        username= user

                    };
                    return activity;

                }
            }
        }

        public async Task LogActivity(Activity activity,string user)
        {
            await client.postToAPI($"Activity/LogActivity?user={user}", activity);
        }
    }
}
