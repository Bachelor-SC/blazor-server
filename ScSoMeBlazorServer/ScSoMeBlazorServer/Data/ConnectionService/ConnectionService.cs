using ScSoMeBlazorServer.Models;
using ScSoMeBlazorServer.Network;
using System.Diagnostics;
using System.Text.Json;

namespace ScSoMeBlazorServer.Data.ConnectionService
{
    public class ConnectionService : IConnectionService
    {
        private readonly IAPIClient client;

        public ConnectionService(IAPIClient client)
        {
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
    }
}
