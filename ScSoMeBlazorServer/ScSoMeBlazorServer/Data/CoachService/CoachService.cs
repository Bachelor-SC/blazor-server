using ScSoMeBlazorServer.Models;
using ScSoMeBlazorServer.Network;
using System.Diagnostics;
using System.Text.Json;

namespace ScSoMeBlazorServer.Data.CoachService
{
    public class CoachService : ICoachService
    {
        private readonly IAPIClient client;

        public CoachService(IAPIClient client)
        {
            this.client = client;
        }
        public async Task<Coach> GetCoach(string coach)
        {
            string json = await client.getFromAPI($"Coaches/Coach?coachToBeFound={coach}");

            return loadActivity(json);

            static Coach loadActivity(string json)
            {
                try
                {
                    Coach? coach = JsonSerializer.Deserialize<Coach>(json);
                    return coach ?? new Coach
                    {
                        username = "No Coaches",
                        name = "Error",
                        shortDesc = "an error occurred",
                        content = "empty list",
                        picture = ""
                    };
                }
                catch (Exception ex)
                {
                    Coach coach = new Coach
                    {
                        username = "No Coaches",
                        name = "Error",
                        shortDesc = "an error occurred",
                        content = "empty list",
                        picture = ""

                    };
                    return coach;

                }
            }
        }

        public async Task<List<Coach>> GetCoaches()
        {
            string json = await client.getFromAPI($"Coaches/AllCoaches");

            return createList(json);

            static List<Coach> createList(string json)
            {
                try
                {
                    List<Coach>? list = JsonSerializer.Deserialize<List<Coach>>(json);
                    return list ?? new List<Coach> {new Coach{
                    username = "No Coaches",
                    name = "Error",
                    shortDesc = "an error occurred",
                    content = "empty list",
                    picture = ""

                }
                };
                }
                catch (Exception ex)
                {
                    List<Coach> list = new List<Coach>();
                    list.Add(new Coach
                    {
                        username = "No Coaches",
                        name = "Error",
                        shortDesc = "an error occurred",
                        content = ex.Message,
                        picture = ""
                    });
                    return list;

                }
            }
        }

        public async Task PostCoach(Coach coach)
        {
            await client.postToAPI($"Coaches/registerCoach", coach);
        }
    }
}
