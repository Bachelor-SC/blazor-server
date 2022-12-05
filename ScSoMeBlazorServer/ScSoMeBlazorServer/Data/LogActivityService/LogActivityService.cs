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
            string json = await client.getFromAPI($"/AllActivities?User={user}");

            return createList(json, user);

            static List<Activity> createList(string json, string user)
            {
                try
                {
                    List<Activity>? list = JsonSerializer.Deserialize<List<Activity>>(json);
                    return list ?? new List<Activity> {new Activity{
                    Type = "Empty",
                    Action = "",
                    Date = DateTime.Now,
                    AdditionalInfo = "List returned empty",
                    Username= user
                    
                }
                };
                }
                catch (Exception ex)
                {
                    List<Activity> list = new List<Activity>();
                    list.Add(new Activity
                    {
                        Type = "Error",
                        Action = "",
                        Date = DateTime.Now,
                        AdditionalInfo = ex.Message,
                        Username= user
                    });
                    return list;

                }
            }
        }

        public async  Task<Activity> GetActivity(string user)
        {
            string json = await client.getFromAPI($"/SingleActivity?User={user}");

            return loadActivity(json, user);

            static Activity loadActivity(string json, string user)
            {
                try
                {
                    Activity? activity = JsonSerializer.Deserialize<Activity>(json);
                    return activity ?? new Activity
                    {
                    Type = "Empty",
                    Action = "",
                        Date = DateTime.Now,
                    AdditionalInfo = "Activity returned empty",
                    Username= user
                    };
                }
                catch (Exception ex)
                {
                    Activity activity = new Activity
                    {
                        Type = "Error",
                        Action = "",
                        Date = DateTime.Now,
                        AdditionalInfo = ex.Message,
                        Username= user

                    };
                    return activity;

                }
            }
        }

        public async Task LogActivity(Activity activity,string user)
        {
            await client.postToAPI($"/LogActivity?user={user}", activity);
        }
    }
}
