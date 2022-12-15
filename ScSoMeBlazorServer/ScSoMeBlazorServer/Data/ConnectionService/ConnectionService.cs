using ScSoMeBlazorServer.Data;
using ScSoMeBlazorServer.Data.LogActivityService;
using ScSoMeBlazorServer.Models;
using ScSoMeBlazorServer.Network;
using System.Diagnostics;
using System.Text.Json;
using Activity = ScSoMeBlazorServer.Models.Activity;

namespace ScSoMeBlazorServer.Data.ConnectionService
{
    public class ConnectionService : IConnectionService
    {
        private readonly IAPIClient client;
        private ILogActivityService activityService;

        public ConnectionService(IAPIClient client, ILogActivityService logActivity)
        {
            activityService = logActivity;
            this.client = client;
        }
        public async Task AddConnection(string username,string connectionUsername)
        {
            Connection con = new Connection
            {
                usernameCon1 = username,
                usernameCon2 = connectionUsername,
                createdDate = DateTime.Now
            };

            await client.postToAPI($"Connections/AddConnection", con);
            await logActivity(username, connectionUsername);
            await logActivity(connectionUsername, username);

        }

        private async Task logActivity(string activityTargetusername, string connectionUsername)
        {
            await activityService.LogActivity(new Activity
            {
                type = "Connection",
                action = "add",
                date = DateTime.Now,
                additionalInfo = $"The user {activityTargetusername} added {connectionUsername} as a connection",
                username = activityTargetusername

            }, activityTargetusername);
        }

        public async Task<List<Connection>> GetAllConnections(string username)
        {
            string json = await client.getFromAPI($"Connections/AllConnections?user={username}");

            return createList(json, username);

            static List<Connection> createList(string json, string user)
            {
                try
                {
                    List<Connection>? list = JsonSerializer.Deserialize<List<Connection>>(json);
                    return list ?? new List<Connection> {new Connection{
                    usernameCon1 = "Empty",
                    createdDate = DateTime.Now,
                    usernameCon2 = "List returned empty"


                }
                };
                }
                catch (Exception ex)
                {
                    List<Connection> list = new List<Connection>();
                    list.Add(new Connection
                    {
                        usernameCon1 = "Error",
                        createdDate = DateTime.Now,
                        usernameCon2 = ex.Message

                    });
                    return list;

                }
            }
        }

        public async Task<bool> CheckIfAlreadyConnected(string username, string connectionUsername)
        {
            string json = await client.getFromAPI($"Connections/CheckIfAlreadyConnected?user={username}&connectionUsername={connectionUsername}");
            return JsonSerializer.Deserialize<bool>(json);

        }

        public async Task DeleteConnection(string username, string connectionUsername)
        {
            await client.deleteFromAPI($"Connections/DeleteConnection?user={username}&connectionUsername={connectionUsername}");
        }

        // BLOCKS SECTION
        public async Task AddBlock(string username, string connectionUsername)
        {
            Connection con = new Connection
            {
                usernameCon1 = username,
                usernameCon2 = connectionUsername,
                createdDate = DateTime.Now
            };

            await client.postToAPI($"Connections/AddBlock", con);
            await DeleteConnection(username,connectionUsername);
            await logBlockActivity(username, connectionUsername,"blocked");
            

        }
        public async Task<List<Connection>> GetBlocksForUser(string username)
        {
            string json = await client.getFromAPI($"Connections/BlocksForUser?user={username}");

            return createList(json, username);

            static List<Connection> createList(string json, string user)
            {
                try
                {
                    List<Connection>? list = JsonSerializer.Deserialize<List<Connection>>(json);
                    return list ?? new List<Connection> {new Connection{
                    usernameCon1 = "Empty",
                    createdDate = DateTime.Now,
                    usernameCon2 = "List returned empty"


                }
                };
                }
                catch (Exception ex)
                {
                    List<Connection> list = new List<Connection>();
                    list.Add(new Connection
                    {
                        usernameCon1 = "Error",
                        createdDate = DateTime.Now,
                        usernameCon2 = ex.Message

                    });
                    return list;

                }
            }
        }

        public async Task<string> CheckIfBlocked(string username, string connectionUsername)
        {
            string json = await client.getFromAPI($"Connections/CheckIfBlocked?user={username}&connectionUsername={connectionUsername}");
            return json;

        }
        public async Task DeleteBlock(string username, string connectionUsername)
        {
            await client.deleteFromAPI($"Connections/DeleteBlock?user={username}&connectionUsername={connectionUsername}");
            await logBlockActivity(username, connectionUsername,"unblocked");
        }

        private async Task logBlockActivity(string activityTargetusername, string blockedUsername,string action)
        {
            await activityService.LogActivity(new Activity
            {
                type = "Block",
                action = "add",
                date = DateTime.Now,
                additionalInfo = $"The user {activityTargetusername} {action} {blockedUsername}",
                username = activityTargetusername

            }, activityTargetusername);
        }


    }
}
