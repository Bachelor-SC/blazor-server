using ScSoMeBlazorServer.Models;

namespace ScSoMeBlazorServer.Data.ConnectionService
{
    public interface IConnectionService
    {
        public Task<List<Connection>> GetAllConnections(string username);

        public Task AddConnection(string username,string connectionUsername);
    }
}
